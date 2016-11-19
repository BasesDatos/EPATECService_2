using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EPATEC_Service_2.Models;
using System.Data.SqlClient;
using System.Data;

namespace EPATEC_Service_2.Controllers
{
    [RoutePrefix("rol")]
    public class RolController : ApiController
    {

        /// <summary>
        /// Método para obtener los roles registrados y que se encuentran disponibles
        /// en la base de datos
        /// </summary>
        /// <returns></returns>
        [Route("getRoles")]
        [HttpGet]
        public IHttpActionResult getRoles()
        {
            List<Rol> roles = new List<Rol>();
            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("Select * from ROL", connection);
                command.CommandType = CommandType.Text;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Rol rol = new Rol();
                        rol._id = reader.GetInt32(0);
                        rol._tipo = reader.GetString(1);
                        roles.Add(rol);
                    }

                    return Json(roles);
                }
                catch(SqlException ex) { return Json(Constants.ERROR_CONNECTION_DATABSE); }
                finally { connection.Close(); }
            }
        }


    }
}
