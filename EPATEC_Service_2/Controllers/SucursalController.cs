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
    [RoutePrefix("offices")]
    public class SucursalController : ApiController
    {

        /// <summary>
        /// Método que permite obtener todas las sucursales registradas de EPATEC
        /// </summary>
        /// <returns></returns>
        [Route("getOffices")]
        [HttpGet]
        public IHttpActionResult getOffices()
        {
            List<Sucursal> offices = new List<Sucursal>();

            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("Select * from SUCURSAL", connection);
                command.CommandType = CommandType.Text;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Sucursal office = new Sucursal();
                        office._id = reader.GetInt32(0);
                        office._nombre = reader.GetString(1);
                        offices.Add(office);
                    }

                    return Json(offices);
                }
                catch(SqlException ex) { return Json(Constants.ERROR_CONNECTION_DATABSE); }
                finally { connection.Close(); }
            }
        }



    }
}
