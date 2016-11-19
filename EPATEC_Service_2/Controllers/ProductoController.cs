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
    [RoutePrefix("products")]
    public class ProductoController : ApiController
    {


        /// <summary>
        /// Método para obtener los productos disponibles en una sucursal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("byOffice/{id}")]
        [HttpPost]
        public IHttpActionResult getProductsByOffice(int id)
        {
            List<Producto> products = new List<Producto>();
            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("dbo.productosPorSucursal", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@idSucursal", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto product = new Producto();
                        product._id = reader.GetInt32(0);
                        product._nombre = reader.GetString(1);
                        product._descripcion = reader.GetString(2);
                        product._exento = reader.GetBoolean(3);
                        product._cantDisponible = reader.GetInt32(4);
                        product._precio = reader.GetDecimal(5);
                        product._categoria = reader.GetString(6);
                        product._proveedor = reader.GetString(7);
                        products.Add(product);
                    }

                    return Json(products);
                }
                catch(SqlException ex) { return Json(Constants.ERROR_CONNECTION_DATABSE); }
                finally { connection.Close(); }
            }
        }


        [Route("getAll")]
        [HttpGet]
        public IHttpActionResult getAll()
        {
            List<Producto> productos = new List<Producto>();
            using(SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("dbo.obtenerProductos", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto._id = reader.GetInt32(0);
                        producto._nombre = reader.GetString(1);
                        producto._descripcion = reader.GetString(2);
                        producto._exento = reader.GetBoolean(3);
                        producto._cantDisponible = reader.GetInt32(4);
                        producto._precio = reader.GetDecimal(5);
                        producto._categoria = reader.GetString(6);
                        producto._sucursal = reader.GetString(7);
                        productos.Add(producto);
                    }
                    return Json(productos);
                }
                catch(SqlException ex) { return Json(Constants.ERROR_CONNECTION_DATABSE); }
                finally { connection.Close(); }
            }
        }


    }
}
