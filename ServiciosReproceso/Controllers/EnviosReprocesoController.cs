using ServiciosReproceso.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace ServiciosReproceso.Controllers
{
    [Authorize]
    public class EnviosReprocesoController : ApiController
    {
        // GET: api/EnviosReproceso
        [HttpGet]
        public List<Reproceso> Get(int idorden)
        {
            //var conectionString = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Reproceso>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();
                // este procedimiento toma informacion de reproceso y reparaciones
                var sqlcommand = new SqlCommand("spdTotalesSiLavado", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.Add("@idorder", SqlDbType.Int).Value = idorden;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Reproceso
                    {
                        idOrder = Convert.ToInt32(dr["Id_Order"].ToString()),
                        talla = dr["Size"].ToString(),
                        totalPorTalla = Convert.ToInt32(dr["Enviado"].ToString()),
                        cantidadDevolucion = Convert.ToInt32("0")
                    };

                    list.Add(obj);
                }
                return list;
            }
        }

        // GET: api/EnviosReproceso
        [HttpGet]
        public List<Reproceso> Get(int idorden, string type)
        {
            // var conectionString = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Reproceso>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdGetReproceso", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.Add("@idOrder", SqlDbType.Int).Value = idorden;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Reproceso
                    {
                        objectId = Convert.ToInt32(dr["id"].ToString()),
                        idOrder = Convert.ToInt32(dr["idC"].ToString()),
                        talla = dr["talla"].ToString(),
                        totalPorTalla = Convert.ToInt32(dr["cantidadDevolucion"].ToString()),// Convert.ToInt32(dr["totalPorTalla"].ToString()),
                        cantidadDevolucion = Convert.ToInt32("0")// Convert.ToInt32("cantidadDevolucion")
                    };

                    list.Add(obj);
                }
                return list;
            }
        }

        // POST: api/EnviosReproceso
        [HttpPost]
        public string Post([FromBody] Reproceso data)
        {
            //var config = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var command = new SqlCommand("spdGuardarReproceso", cn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@idOrder", SqlDbType.Int).Value = data.idOrder;
                command.Parameters.Add("@talla", SqlDbType.NChar, 10).Value = data.talla;
                command.Parameters.Add("@totalXtalla", SqlDbType.Int).Value = data.totalPorTalla;
                command.Parameters.Add("@cantdev", SqlDbType.Int).Value = data.cantidadDevolucion;
                command.Parameters.Add("@observacion", SqlDbType.NVarChar, 1000).Value = data.observacion;
                command.Parameters.Add("@usuario", SqlDbType.NChar, 20).Value = data.usuario;

                command.ExecuteNonQuery();

                return "Ok";
            }

        }

        // GET: api/EnviosReproceso
        [HttpPost]
        public string Post([FromBody] Reproceso data, int id)
        {
            //var config = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var command = new SqlCommand("spdGuardarDetReproceso", cn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@idObjectReproceso", SqlDbType.Int).Value = id;// data.objectIdReproceso;
                command.Parameters.Add("@cantidadRetorno  ", SqlDbType.Int).Value = data.cantidadDevolucion;
                command.Parameters.Add("@Observacion ", SqlDbType.NVarChar).Value = data.observacion;
                command.Parameters.Add("@usuario ", SqlDbType.NChar, 20).Value = data.usuario;

                command.ExecuteNonQuery();

                return "Ok";
            }

        }

        // PUT: api/EnviosReproceso/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/EnviosReproceso/5
        public void Delete(int id)
        {
        }
    }
}
