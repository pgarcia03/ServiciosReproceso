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
    public class ReprocesosPlanchaController : ApiController
    {
        // disponible para enviar a reproceso de plancha
        // GET: api/ReprocesosPlancha
        public List<disponiblePlancha> Get()
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<disponiblePlancha>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                string query = "select * from vwIntexGetDisponibleEnvioPlancha";

                var sqlcommand = new SqlCommand(query, cn);
                sqlcommand.CommandType = CommandType.Text;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new disponiblePlancha
                    {
                        // objectId = Convert.ToInt32(dr["id"].ToString()),
                        corte = dr["PorderClient"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        Waist = Convert.ToInt32(dr["Waist"].ToString()),
                        Inseam = Convert.ToInt32(dr["Inseam"].ToString()),
                        WI = Convert.ToInt32(dr["WI"].ToString()),
                        // color = dr["color"].ToString(),
                        //unidadesCorte = Convert.ToInt32(dr["unidades"].ToString()),
                        disponible = Convert.ToInt32(dr["Total"].ToString()),
                        uniTransferencia = 0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        //detalle de tolereancias con medidas fuera de tolerancia menor
        // GET: api/ReprocesosPlancha/5
        public List<disponiblePlancha> Get(string corte)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<disponiblePlancha>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

          
                var sqlcommand = new SqlCommand("spdIntexGetDetalleDisponiblePlancha", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.Add("@corteParam", SqlDbType.NChar,25).Value = corte;


                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new disponiblePlancha
                    {
                        // objectId = Convert.ToInt32(dr["id"].ToString()),
                        corte = dr["POrderClient"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        tolerancia= dr["tolerancia"].ToString(),
                        Waist = Convert.ToInt32(dr["Waist"].ToString()),
                        Inseam = Convert.ToInt32(dr["Inseam"].ToString()),
                        WI = Convert.ToInt32(dr["WI"].ToString()),
                        // color = dr["color"].ToString(),
                        //unidadesCorte = Convert.ToInt32(dr["unidades"].ToString()),
                        disponible = Convert.ToInt32(dr["Total"].ToString()),
                        // uniTranferencia = 0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        /// <summary>
        /// unidades disponibles para dar de baja del area de reproceso de plancha
        /// </summary>
        /// <param name="corte"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ReprocesosPlancha/5
        public List<disponiblePlancha> Get(string corte,int id)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<disponiblePlancha>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();


                var sqlcommand = new SqlCommand("spdIntexGetEnPlanchaMedido", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;
              //  sqlcommand.Parameters.Add("@corte", SqlDbType.Int).Value = corte;


                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new disponiblePlancha
                    {
                        objectId = Convert.ToInt32(dr["id"].ToString()),
                        corte = dr["corte"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        color = dr["color"].ToString(),
                        disponible = Convert.ToInt32(dr["Unidades"].ToString()),
                        uniTransferencia = 0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        // POST: api/ReprocesosPlancha
        public string Post([FromBody] disponiblePlancha data)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexIngresoUnidadesPlancha", cn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@corteParam", SqlDbType.NVarChar).Value = data.corte;
                    command.Parameters.Add("@unidadesParam", SqlDbType.Int).Value = data.uniTransferencia;

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
        /// Dar de baja Reproceso de plancha
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: api/ReprocesosPlancha
        public string Post([FromBody] disponiblePlancha data,int id)
        {
            try
            {
                var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

                using (SqlConnection cn = new SqlConnection(conectionString))
                {
                    cn.Open();

                    var command = new SqlCommand("spdIntexSalidaRepPlancha", cn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@corteParam", SqlDbType.NVarChar).Value = data.corte;
                    command.Parameters.Add("@unidadesParam", SqlDbType.Int).Value = data.uniTransferencia;

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
