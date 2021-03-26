using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ServiciosReproceso.Models
{
    public class DBAuditoriaConection : ConexionDB
    {       
        public override string conectionstring()
        {
            return ConfigurationManager.ConnectionStrings["cadenaconexion"].ConnectionString; 
        }

    }

    public class DBPackingConection : ConexionDB
    {      
        public override string conectionstring()
        {
            return ConfigurationManager.ConnectionStrings["seguridad"].ConnectionString; 
        }

    }

}