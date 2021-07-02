using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class Disponible
    {
        public int objectId { get; set; }
        public string corte { get; set; }
        public string estilo{ get; set; }
        public string color { get; set; }
        public int unidadesCorte{ get; set; }
        public int disponible { get; set; }
        public int uniTranferencia { get; set; }
    }
}