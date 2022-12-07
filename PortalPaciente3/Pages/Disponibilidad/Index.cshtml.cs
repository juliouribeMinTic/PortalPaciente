using PortalPaciente3.Models;
using HomilApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace PortalPaciente3.Pages.Disponibilidad
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly HomilClient homilClient = new HomilClient();
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            ListEspecialidades = new List<EspecialidadItem>();
        }
        public async Task OnGet()
        {
            var json = JsonConvert.SerializeObject(new { userName = _configuration["HomilApi:username"], password = _configuration["HomilApi:password"] });
            responseAuth response = await homilClient.executeRequestPost<responseAuth>("/Auth/createToken", json, HttpContext);
            if (response.statusCode == 200)
            {
                HttpContext.Session.SetString("token", response.value);
                ResponseHomil<Especialidad> responseListESp = await homilClient.executeRequestGet<ResponseHomil<Especialidad>>("/Citas/EHR/ListarEspecialidades", "", HttpContext, 1);
                if (responseListESp.sucess)
                {
                    ListEspecialidades = responseListESp.result.especialidad.ToList();
                }
            }
        }

        public async Task<IActionResult> OnPostConsultarProfecionalesAsync([FromBody] DisponibilidadModel collection)
        {
            var paramsQuery = $"FechaDesde={collection.FechaInicial.ToString("yyyy-MM-dd")}&FechaHasta={collection.FechaInicial.AddDays(30).ToString("yyyy-MM-dd")}&OIDEspecialidad={collection.Especialidad}";
            ResponseHomil<Profecional> responseListProfecionales = await homilClient.executeRequestGet<ResponseHomil<Profecional>>("/Citas/EHR/ListarDisponibilidadProfesional", paramsQuery, HttpContext,1);

            return new JsonResult(responseListProfecionales);
        }
        public async Task<IActionResult> OnPostConsultarDisponibilidadAsync([FromBody] DisponibilidadModel collection)
        {
            var paramsQuery = $"FechaDesde={collection.FechaInicial.ToString("yyyy-MM-dd")}&FechaHasta={collection.FechaInicial.AddDays(30).ToString("yyyy-MM-dd")}&OIDProfesional={collection.Profecional}";
            ResponseHomil<PortalPaciente3.Models.Disponibilidad> responseListTurnosDisponibles = await homilClient.executeRequestGet<ResponseHomil<PortalPaciente3.Models.Disponibilidad>>("/Citas/EHR/ListarDisponibilidadFecha", paramsQuery, HttpContext, 1);
            return new JsonResult(responseListTurnosDisponibles);
        }
        public int Especialidad { get; set; }

        public DateTime FechaInicial { get; set; }

        public int Profecional { get; set; }
        public List<EspecialidadItem> ListEspecialidades { get; set; }

    }
}
