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
    public class HeatTransfersController : ApiController
    {
        // disponible para enviar a proceso de heatTransfer
        // GET: api/HeatTransfers
        public List<Disponible> Get()
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Disponible>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                                                
                var sqlcommand = new SqlCommand("spdIntexGetDisponibleIngresoHeat", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Disponible
                    {
                        // objectId = Convert.ToInt32(dr["id"].ToString()),
                        corte = dr["corte"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        color = dr["color"].ToString(),
                        disponible = Convert.ToInt32(dr["unidades"].ToString()),
                        // uniTranferencia = 0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        //inventario en heat Transfer
        // GET: api/HeatTransfers/5
        public List<Disponible> Get(int id)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Disponible>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();


                var sqlcommand = new SqlCommand("spdIntexGetDisponibleEnHeat", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;
            

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Disponible
                    {
                       
                        corte = dr["corte"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        color = dr["color"].ToString(),
                      
                        disponible = Convert.ToInt32(dr["Unidades"].ToString()),
                      
                    };

                    list.Add(obj);
                }

                return list;
            }
        }
        
        /// <summary>
        /// envio de unidades a heatTR
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: api/HeatTransfers
        public string Post([FromBody] ProcesoHeatTransfer data)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexIngresoHeat", cn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@corteParam", SqlDbType.NVarChar).Value = data.corte;
                    command.Parameters.Add("@unidadesParam", SqlDbType.Int).Value = data.unidades;

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

        /// <summary>
        /// r3gistro de uniadades dañadas en heat transfer
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: api/HeatTransfers
        public string Post([FromBody] ProcesoHeatTransfer data,int id)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexIngresoUnidaesDefecHeat", cn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@corteParam", SqlDbType.NVarChar).Value = data.corte;
                    command.Parameters.Add("@unidadesParam", SqlDbType.Int).Value = data.unidades;

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


        /// <summary>
        /// Dar de baja en heat transfer
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: api/HeatTransfers
        public string Post([FromBody] ProcesoHeatTransfer data, int id,int id2)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexSalidaHeat", cn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@corteParam", SqlDbType.NVarChar).Value = data.corte;
                    command.Parameters.Add("@unidadesParam", SqlDbType.Int).Value = data.unidades;

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


        // PUT: api/ReprocesosPlancha/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ReprocesosPlancha/5
        public void Delete(int id)
        {
        }
    }
}
