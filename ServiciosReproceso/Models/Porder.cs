using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class Porder
    {
        public int idCorte{ get; set; }
        public string corte { get; set; }
        public int cantidad { get; set; }
        public string estilo { get; set; }
        public bool washed { get; set; }
    }
}