﻿using ServiciosReproceso.Models;
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
    public class IrregularesController : ApiController
    {
        // GET: api/Irregulares
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Irregulares/5
        public List<Irregular> Get(int id)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Irregular>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdGetListaIrregularesXidCorte", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.Add("@idOrder", SqlDbType.Int).Value = id;

                var dr = sqlcommand.ExecuteReader();


                while (dr.Read())
                {
                    var obj = new Irregular
                    {
                        idOrder = Convert.ToInt32(dr["idOrder"].ToString()),
                        talla = dr["talla"].ToString(),
                        idDefecto = Convert.ToInt32(dr["idDefecto"].ToString()),
                        idPosicion = Convert.ToInt32(dr["idPosicion"].ToString()),
                        unidades = Convert.ToInt32(dr["total"].ToString()),
                        defecto = dr["Defecto"].ToString(),
                        posicion = dr["Posicion"].ToString(),
                        area = dr["area"].ToString()

                    };

                    list.Add(obj);

                }

                return list;

            }
        }

        // POST: api/Irregulares
        [HttpPost]
        [Route("api/Irregulares/crear")]
        public string Post([FromBody]Irregular data)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var command = new SqlCommand("spdSaveIrregulares", cn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@idOrder", SqlDbType.Int).Value = data.idOrder;
                command.Parameters.Add("@idDefecto", SqlDbType.Int).Value = data.idDefecto;
                command.Parameters.Add("@idPosicion", SqlDbType.Int).Value = data.idPosicion;
                command.Parameters.Add("@talla", SqlDbType.NChar,15).Value = data.talla;
                command.Parameters.Add("@color", SqlDbType.NChar,15).Value = data.color;
                command.Parameters.Add("@area", SqlDbType.NVarChar, 2).Value = data.area;
                command.Parameters.Add("@usuario", SqlDbType.NChar, 35).Value = data.usuarioRegistro;
                command.Parameters.Add("@total", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();

                var total = int.Parse(command.Parameters["@total"].Value.ToString());
                return "Ok";
            }

        }

        // PUT: api/Irregulares/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Irregulares/5
        [HttpPost]
        [Route("api/Irregulares/eliminar")]
        public string Delete(int id,[FromBody] Irregular data)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();
           
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var command = new SqlCommand("spdDeleteIrregulares", cn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@idOrder", SqlDbType.Int).Value = data.idOrder;
                command.Parameters.Add("@idDefecto", SqlDbType.Int).Value = data.idDefecto;
                command.Parameters.Add("@idPosicion", SqlDbType.Int).Value = data.idPosicion;
                command.Parameters.Add("@talla", SqlDbType.NChar,15).Value = data.talla;
                command.Parameters.Add("@usuario", SqlDbType.NChar, 35).Value = data.usuarioRegistro;
                command.Parameters.Add("@area", SqlDbType.NChar, 2).Value = data.area;
                command.Parameters.Add("@total", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();

                var total = int.Parse(command.Parameters["@total"].Value.ToString());
                return "Ok";
            }
        }
    }
}
