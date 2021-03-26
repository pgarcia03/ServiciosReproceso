using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class CreadorConection
    {
        public const int Auditoria = 1;
        public const int Packing = 2;

        public static ConexionDB Creador(int tipo)
        {
            switch (tipo)
            {
                case Auditoria:
                    return new DBAuditoriaConection();
                   
                case Packing:
                    return new DBPackingConection();
                   
                default:
                    return null;
            }
        }

    }
}