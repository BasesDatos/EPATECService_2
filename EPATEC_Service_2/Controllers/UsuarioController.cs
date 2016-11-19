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
    [RoutePrefix("users")]
    public class UsuarioController : ApiController
    {

        /// <summary>
        /// Método para registrar un usuario en general
        /// El rol con el cual se esta registrando el usuario se especifica por medio
        /// del parametro "_rol"; el cual debe ser un rol registrado en la base de datos
        /// </summary>
        /// <param name="pUser"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public IHttpActionResult register(Usuario pUser)
        {
            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("dbo.registrarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@usuario", SqlDbType.VarChar).Value = pUser._usuario;
                command.Parameters.AddWithValue("@contrasena", SqlDbType.VarChar).Value = pUser._contrasena;
                command.Parameters.AddWithValue("@cedula", SqlDbType.BigInt).Value = pUser._cedula;
                command.Parameters.AddWithValue("@nombre", SqlDbType.VarChar).Value = pUser._nombre;
                command.Parameters.AddWithValue("@pApellido", SqlDbType.VarChar).Value = pUser._pApellido;
                command.Parameters.AddWithValue("@sApellido", SqlDbType.VarChar).Value = pUser._sApellido;
                command.Parameters.AddWithValue("@fNacimiento", SqlDbType.Date).Value = pUser._fNacimiento.Date;
                command.Parameters.AddWithValue("@telefono", SqlDbType.VarChar).Value = pUser._telefono;
                command.Parameters.AddWithValue("@provincia", SqlDbType.VarChar).Value = pUser._provincia;
                command.Parameters.AddWithValue("@canton", SqlDbType.VarChar).Value = pUser._canton;
                command.Parameters.AddWithValue("@distrito", SqlDbType.VarChar).Value = pUser._distrito;
                command.Parameters.AddWithValue("@rol", SqlDbType.VarChar).Value = pUser._rol;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    int result = reader.GetInt32(0);

                    return Json(result);
                }
                catch(SqlException ex) { return Json(ex.Message); }
                finally { connection.Close(); }
            }

        }
    }
}
