using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class ReprocesoDet
    {
        public int idObjectReproceso { get; set; }
        public Nullable<int> cantidadRetorno { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }
    }
}