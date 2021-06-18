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
    //[Authorize]
    public class PosicionsController : ApiController
    {
        // GET: api/Posicions
        [HttpGet]
        public List<Posicion> Get()
        {
            // var conectionString = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Posicion>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("select idPosicion,Posicion from tbPosicion where irregular=1 order by Posicion", cn);
                sqlcommand.CommandType = CommandType.Text;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Posicion
                    {
                        idPosicion = Convert.ToInt32(dr["idPosicion"].ToString()),
                        nombre = dr["Posicion"].ToString(),
                   
                    };

                    list.Add(obj);

                }

                return list;

            }

        }

        // GET: api/Posicions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Posicions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Posicions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Posicions/5
        public void Delete(int id)
        {
        }
    }
}
