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
    public class DisponibleEnMedidasController : ApiController
    {
        // GET: api/DisponibleEnMedidas
        public List<Disponible> Get()
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Disponible>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdIntexGetUnidadesMedida", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Disponible
                    {
                        objectId=Convert.ToInt32(dr["id"].ToString()),
                        corte = dr["corte"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        color = dr["color"].ToString(),
                        //unidadesCorte = Convert.ToInt32(dr["unidades"].ToString()),
                        disponible = Convert.ToInt32(dr["disponible"].ToString()),
                       // uniTranferencia = 0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        // GET: api/DisponibleEnMedidas/5
        public List<Disponible> Get(string corte)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Disponible>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

              //  string query= $"select * from vwGetCortesMedidaG where POrderClient='{ corte }'";

                var sqlcommand = new SqlCommand("spdIntexGetMedidaDisponible", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.Add("@corteParam", SqlDbType.NChar,20).Value = corte;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Disponible
                    {
                        corte = dr["POrderClient"].ToString(),
                        estilo = dr["Medida"].ToString(),
                        color = dr["tolerancia"].ToString(),
                        disponible = Convert.ToInt32(dr["unidades"].ToString()),
                        //disponible = Convert.ToInt32(dr["disponible"].ToString()),
                        uniTransferencia = 0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        // POST: api/DisponibleEnMedidas
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DisponibleEnMedidas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DisponibleEnMedidas/5
        public void Delete(int id)
        {
        }
    }
}
