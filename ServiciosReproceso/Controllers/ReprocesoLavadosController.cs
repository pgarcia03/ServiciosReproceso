using ServiciosReproceso.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace ServiciosReproceso.Controllers
{
    public class ReprocesoLavadosController : ApiController
    {
        // GET: api/ReprocesoLavados
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ReprocesoLavados/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ReprocesoLavados
        public string Post([FromBody] ReprocesoLavado data)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexIngresoReprocesoLavado", cn);
                    command.CommandType = CommandType.StoredProcedure;

                   // command.Parameters.Add("@idInventarioMedidasParam", SqlDbType.Int).Value = data.id;
                    command.Parameters.Add("@unidadesParam", SqlDbType.Int).Value = data.unidades;
                    command.Parameters.Add("@xmlstringParam", SqlDbType.NVarChar).Value = data.xml;
                  

                    command.ExecuteNonQuery();

                    return "Ok";
                }
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
                return ex.Message;
            }

        }

        // PUT: api/ReprocesoLavados/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReprocesoLavados/5
        public void Delete(int id)
        {
        }
    }
}
