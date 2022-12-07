
$(document).ready(function () {
    $("#especialidad").on("change", async function (event) {
        let data = {
                especialidad: event.target.value,
                fechaInicial: $("#fechainicial").val()
        }
        let responsePorfecionales = await $.ajax({
            url: "/Disponibilidad/Index?handler=ConsultarProfecionales",
            data: JSON.stringify(data),
            type: "POST",
            dataType: 'json',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "RequestVerificationToken": $("#__RequestVerificationToken").val()
            }
        });
        if (responsePorfecionales.sucess) {
            let profecionales = responsePorfecionales.result.profesional;
            let hrml = "";
            for (let item of profecionales) {
                hrml += `<option value="${item.oid}">${item.gmenomcom}</option>`;
            }
            $("#profecional").html(hrml);
        }
    });
    $("#consultar").on("click", async function () {
        let data = {
            especialidad: $("#especialidad").val(),
            fechaInicial: $("#fechainicial").val(),
            profecional: $("#profecional").val()
        };
        let responseDisponibles = await $.ajax({
            url: "/Disponibilidad/Index?handler=ConsultarDisponibilidad",
            data: JSON.stringify(data),
            type: "POST",
            dataType: 'json',
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "RequestVerificationToken": $("#__RequestVerificationToken").val()
            }
        });
        if (responseDisponibles.sucess) {
            let disponibles = responseDisponibles.result.citafecha;
            let hrml = "";
            for (let item of disponibles) {
                hrml += `<li>${item.oidTurno}</li>`;
            }
            $("#disponibilidad").html(hrml);
        }
    });
     
});

