using ServiciosReproceso.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace ServiciosReproceso.Controllers
{
    //[Authorize]
    public class PordersController : ApiController
    {
        // GET: api/Porders/pre
        [HttpGet]
        public List<Porder> Get(string pre)
        {
           // var conectionString = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Porder>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open(); 

                var sqlcommand = new SqlCommand("spdBuscarPoCliente", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.Add("@porder", SqlDbType.NChar, 15).Value = pre;

                var dr = sqlcommand.ExecuteReader();


                while (dr.Read())
                {
                    var obj = new Porder
                    {
                        //idCorte = Convert.ToInt32(dr["Id_Order"].ToString()),
                        corte = dr["Porder"].ToString(),
                        estilo = dr["Style"].ToString(),
                        cantidad = Convert.ToInt32(dr["Quantity"].ToString()),
                        washed = Convert.ToBoolean(dr["Washed"]),
                        descrip= dr["Describir"].ToString()
                        
                    };

                    list.Add(obj);

                }

                return list;

            }

        }

        [HttpGet]
        public List<Linea> Get()
        {
           // var conectionString = Coneccion.Cadena.conexion;
            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Linea>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("select id_linea as id,numero as linea from Linea where numero not like '%-%' order by convert(float, numero) asc", cn);
                sqlcommand.CommandType = CommandType.Text;

                var dr = sqlcommand.ExecuteReader();


                while (dr.Read())
                {
                    var obj = new Linea
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        linea = dr["linea"].ToString()

                    };

                    list.Add(obj);

                }

                return list;

            }

        }



        //  GET: api/Porders/5
        [HttpGet]
        [Route("api/porders/getTallas")]
        public List<Porder> Get(string corte,int i=1)
        {

            var conectionString = CreadorConection.Creador(CreadorConection.Auditoria).conectionstring();

            var list = new List<Porder>();
            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("spdGetTallasUniXCorteCliente", cn);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.Add("@corte", SqlDbType.NChar,25).Value = corte;

                var dr = sqlcommand.ExecuteReader();


                while (dr.Read())
                {
                    var obj = new Porder
                    {
                        idCorte = Convert.ToInt32(dr["unidades"].ToString()),
                        corte = dr["Size"].ToString(),
                      
                    };

                    list.Add(obj);

                }

                return list;

            }

           

        }

        // POST: api/Porders
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Porders/5
        //[HttpPut]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Porders/5
        //[HttpDelete]
        //public void Delete(int id)
        //{
        //}
    }
}
