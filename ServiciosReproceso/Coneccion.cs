using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ServiciosReproceso
{
    // patron de diseño singleton buena practica para manejo de cadena de conexion a sql server
    public class Coneccion
    {
        private static Coneccion cadena = null;
        public string conexion;

        protected Coneccion() {
            this.conexion = ConfigurationManager.ConnectionStrings["cadenaconexion"].ConnectionString;
        }

        public static Coneccion Cadena
        {
            get
            {
                if (cadena==null)
                {
                    cadena = new Coneccion();

                }

                return cadena;
            }
        }
    } 

}