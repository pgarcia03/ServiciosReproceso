using ServiciosReproceso.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiciosReproceso.Controllers
{
  // [Authorize]
    public class DefectosController : ApiController
    {
        [HttpGet]
        public List<Defecto> Get()
        {
            // var conectionString = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Defecto>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("select idDefecto,Defecto from tbDefectos where irregular=1 order by Defecto", cn);
                sqlcommand.CommandType = CommandType.Text;

                var dr = sqlcommand.ExecuteReader();


                while (dr.Read())
                {
                    var obj = new Defecto
                    {
                        idDefecto = Convert.ToInt32(dr["idDefecto"].ToString()),
                        nombre = dr["Defecto"].ToString(),

                    };

                    list.Add(obj);

                }

                return list;

            }

        }
    }
}
