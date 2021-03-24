using ServiciosReproceso.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace ServiciosReproceso.Controllers
{
    public class EnviosReparacionController : ApiController
    {
        // GET: api/EnviosReparacion
        [HttpGet]
        public List<Reparacion> Get(int idorden)
        {
            var conectionString = Coneccion.Cadena.conexion;

            var list = new List<Reparacion>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdTotalesNoLavadoRepa", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.Add("@idorder", SqlDbType.Int).Value = idorden;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Reparacion
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

        // GET: api/EnviosReparacion
        [HttpGet]
        public List<Reproceso> Get(int idorden, string type)
        {
            var conectionString = Coneccion.Cadena.conexion;

            var list = new List<Reproceso>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdGetReparaciones", cn);
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

        // GET: api/EnviosReparacion
        [HttpPost]
        public string Post([FromBody] Reparacion data)
        {
            var config = Coneccion.Cadena.conexion;

            using (SqlConnection cn = new SqlConnection(config))
            {
                cn.Open();

                var command = new SqlCommand("spdGuardarReparaciones", cn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@idOrder", SqlDbType.Int).Value = data.idOrder;
                command.Parameters.Add("@talla", SqlDbType.NChar, 10).Value = data.talla;
                command.Parameters.Add("@idlinea", SqlDbType.Int).Value = data.idLinea;
                command.Parameters.Add("@totalXtalla", SqlDbType.Int).Value = data.totalPorTalla;
                command.Parameters.Add("@cantdev", SqlDbType.Int).Value = data.cantidadDevolucion;
                command.Parameters.Add("@observacion", SqlDbType.NVarChar, 1000).Value = data.observacion;
                command.Parameters.Add("@usuario", SqlDbType.NChar, 20).Value = data.usuario;

                command.ExecuteNonQuery();

                return "Ok";
            }

        }

        // GET: api/EnviosReparacion
        [HttpPost]
        public string Post([FromBody] Reparacion data, int id)
        {
            var config = Coneccion.Cadena.conexion;

            using (SqlConnection cn = new SqlConnection(config))
            {
                cn.Open();

                var command = new SqlCommand("spdGuardarDetReparaciones", cn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@idObjectRepa", SqlDbType.Int).Value = id;// data.objectIdReparaciones;
                command.Parameters.Add("@cantidadRet ", SqlDbType.Int).Value = data.cantidadDevolucion;
                command.Parameters.Add("@observacion ", SqlDbType.NVarChar).Value = data.observacion;
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
