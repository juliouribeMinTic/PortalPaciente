using System;
using System.Collections.Generic;
using System.Text;

namespace PortalPaciente3.Models
{
    public class ResponseHomil<T>
    {
        public bool sucess { get; set; }
        public string message { get; set; }
        public T result { get; set; }
    }
}
