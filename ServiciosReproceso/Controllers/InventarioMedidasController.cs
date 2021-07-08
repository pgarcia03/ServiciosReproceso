using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ServiciosReproceso.Models;

namespace ServiciosReproceso.Controllers
{
    [AllowAnonymous]
    public class InventarioMedidasController : ApiController
    {
        // GET: api/InventarioMedidas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/InventarioMedidas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InventarioMedidas
        public string Post([FromBody]Disponible data)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexIgresoInventarioAreaMedidas", cn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@corteParam", SqlDbType.NChar,15).Value = data.corte;// data.objectIdReproceso;
                    command.Parameters.Add("@estiloParam", SqlDbType.NChar,15).Value = data.estilo;
                    command.Parameters.Add("@colorParam", SqlDbType.NChar,15).Value = data.color;
                    command.Parameters.Add("@unidadesCorteParam", SqlDbType.Int).Value = data.unidadesCorte;
                    command.Parameters.Add("@unidadesTransferenciaParam", SqlDbType.Int).Value = data.uniTransferencia;

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

        public string Post([FromBody] Disponible data,int id)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexIgresoInventarioDevolucion", cn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@corteParam", SqlDbType.NChar, 15).Value = data.corte;
                    command.Parameters.Add("@estiloParam", SqlDbType.NChar, 15).Value = data.estilo;
                    command.Parameters.Add("@colorParam", SqlDbType.NChar, 15).Value = data.color;
                    command.Parameters.Add("@unidadesParam", SqlDbType.Int).Value = data.uniTransferencia;

                    command.ExecuteNonQuery();

                    return "Ok";
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return ex.Message;
            }
           
        }

        // PUT: api/InventarioMedidas/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/InventarioMedidas/5
        public void Delete(int id)
        {
        }
    }
}
