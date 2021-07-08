using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class Irregular
    {
        public int objectIdIrregulares { get; set; }
        public int idOrder { get; set; }
        public int idDefecto { get; set; }
        public int idPosicion { get; set; }
        public string talla { get; set; }
        public string color { get; set; }
        public int unidades { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string area { get; set; }
        public string usuarioRegistro { get; set; }


        public string defecto { get; set; }
        public string posicion { get; set; }

        //nuevo campo
        public string corte { get; set; }


    }
}