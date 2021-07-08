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
    public class DisponiblesConteoController : ApiController
    {
        // GET: api/DisponiblesConteo
        public List<Disponible> Get()
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Disponible>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdIntexDisponibleConteo", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Disponible
                    {
                        corte = dr["corte"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        color = dr["color"].ToString(),
                        unidadesCorte=Convert.ToInt32(dr["unidades"].ToString()),
                        disponible= Convert.ToInt32(dr["disponible"].ToString()),
                        uniTransferencia=0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        // GET: api/DisponiblesConteo/5  spdDisponibleRepLavado
        public List<Disponible> Get(int id)
        {
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Disponible>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdIntexDisponibleRepLavado", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var obj = new Disponible
                    {
                        corte = dr["corte"].ToString(),
                        estilo = dr["estilo"].ToString(),
                        color = dr["color"].ToString(),
                        disponible = Convert.ToInt32(dr["disponible"].ToString()),
                        uniTransferencia=0

                    };

                    list.Add(obj);
                }

                return list;
            }
        }

        // POST: api/DisponiblesConteo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DisponiblesConteo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DisponiblesConteo/5
        public void Delete(int id)
        {
        }
    }
}
