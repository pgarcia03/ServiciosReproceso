using ServiciosReproceso.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Web.Http;

namespace ServiciosReproceso.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginsController : ApiController
    {
       

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate([FromBody] Usuario login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = false;// (login.Password == "123456");
           
            var conectionString = CreadorConection.Creador(CreadorConection.Packing).conectionstring();

            using (SqlConnection cn = new SqlConnection(conectionString))
            {
                cn.Open();

                var sqlcommand = new SqlCommand("select isnull(idUser,0) as id from tbUser where nombre='" + login.Username + "' and CodigoAcceso='" + login.Password + "'", cn);
                sqlcommand.CommandType = CommandType.Text;

                var dr = sqlcommand.ExecuteReader();

                while (dr.Read())
                {
                    var id = Convert.ToInt32(dr["id"].ToString());

                    isCredentialValid = id > 0 ? true : false;
                }

                if (isCredentialValid)
                {
                    var token = TokenGenerator.GenerateTokenJwt(login.Username);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }

            }


        }
    }
}
