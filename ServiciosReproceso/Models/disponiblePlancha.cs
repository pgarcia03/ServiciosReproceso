using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class disponiblePlancha:Disponible
    {
        public string tolereancia { get; set; }
        public int Waist { get; set; }
        public int Inseam { get; set; }
        public int WI { get; set; }
    }
}