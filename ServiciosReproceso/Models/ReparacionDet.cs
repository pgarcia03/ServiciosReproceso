using System;

namespace ServiciosReproceso.Models
{
    public class ReparacionDet
    {
        public int idObjectReparaciones { get; set; }
        public Nullable<int> cantidadRetorno { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }
    }
}