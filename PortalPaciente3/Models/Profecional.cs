using System;
using System.Collections.Generic;
using System.Text;

namespace PortalPaciente3.Models
{
    public class Profecional
    {
        public List<ProfecionalItem> profesional { get; set; }
}
    public class ProfecionalItem
    {
        public string gmenomcom { get; set; }
        public int oid { get; set; }
    }
}
