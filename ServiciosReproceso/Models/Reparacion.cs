using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class Reparacion
    {

        public int objectId { get; set; }
        public int objectIdReparaciones { get; set; }
        public int idOrder { get; set; }
        public int idLinea { get; set; }
        public string talla { get; set; }
        public Nullable<int> totalPorTalla { get; set; }
        public Nullable<int> cantidadDevolucion { get; set; }
        public Nullable<bool> estado { get; set; }
        public string observacion { get; set; }
        public Nullable<System.DateTime> fechaDevolucion { get; set; }
        public string usuario { get; set; }

    }
}