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
    [RoutePrefix("orders")]
    public class PedidoController : ApiController
    {

        /// <summary>
        /// Método para registrar un pedido
        /// Llama al método auxiliar "registerAux" el cual registra los detalles del pedido
        /// luego llama al método "addProductToOrder" el cual registra los productos del pedido
        /// </summary>
        /// <param name="pOrder"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public IHttpActionResult register(Pedido pOrder)
        {
            int resultInsertOrder = this.registerAux(pOrder);
            if (resultInsertOrder > 0)
            {
                int resultInsertProducts = this.addProductToOrder(pOrder._productos, resultInsertOrder);
                if (resultInsertProducts > 0) return Json(1);
                else return Json(-1);
            }
            else return Json(-2);
        }


        /// <summary>
        /// Método auxiliar que registra los detalles del pedido
        /// </summary>
        /// <param name="pOrder"></param>
        /// <returns></returns>
        public int registerAux(Pedido pOrder)
        {
            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("dbo.registrarPedido", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@fecha", SqlDbType.Date).Value = pOrder._fechaHora.Date;
                command.Parameters.AddWithValue("@hora", SqlDbType.Time).Value = pOrder._fechaHora.ToString("HH:mm");
                command.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = pOrder._total;
                command.Parameters.AddWithValue("@cliente", SqlDbType.VarChar).Value = pOrder._cliente;
                command.Parameters.AddWithValue("@vendedor", SqlDbType.VarChar).Value = pOrder._vendedor;
                command.Parameters.AddWithValue("@idSucursal", SqlDbType.Int).Value = pOrder._sucursal;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    int result = reader.GetInt32(0);

                    return result;
                }
                catch (SqlException ex) { return Constants.ERROR_CONNECTION_DATABSE; }
                finally { connection.Close(); }
            }
        }



        /// <summary>
        /// Método que registra los productos del pedido
        /// </summary>
        /// <param name="pProducts"></param>
        /// <param name="pOrderId"></param>
        /// <returns></returns>
        public int addProductToOrder(List<ProductoPedido> pProducts, int pOrderId)
        {
            using (SqlConnection connection = DataBase.getConnection())
            {
               
                try
                {
                    int counter = 0;
                    for (int i = 0; i < pProducts.Count; i++)
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("dbo.productosPedido", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idPedido", SqlDbType.Int).Value = pOrderId;
                        command.Parameters.AddWithValue("@idProducto", SqlDbType.Int).Value = pProducts.ElementAt(i)._id;
                        command.Parameters.AddWithValue("@cantidad", SqlDbType.Int).Value = pProducts.ElementAt(i)._cantidad;

                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();

                        if(reader.GetInt32(0) > 0) { counter++; }
                        connection.Close();
                    }

                    if (counter != pProducts.Count)
                        return -1;
                    else return 1;
                }
                catch(SqlException ex) { return Constants.ERROR_CONNECTION_DATABSE; }
                finally { connection.Close(); }
            }
        }


        /// <summary>
        /// Método para obtener todos los productos registrados
        /// </summary>
        /// <returns></returns>
        [Route("getAll")]
        [HttpGet]
        public IHttpActionResult getAll()
        {
            List<Pedido> orders = new List<Pedido>();
            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("dbo.obtenerPedidos", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Pedido order = new Pedido();
                        order._id = reader.GetInt32(0);
                        string date = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                        //string hour = reader.GetDateTime(2).ToString("HH:mm:ss");
                        order._fechaHora = DateTime.Parse(date);
                        order._total = reader.GetDecimal(3);
                        order._estado = reader.GetBoolean(4);
                        order._cliente = reader.GetString(5);
                        order._nombreSucursal = reader.GetString(6);
                        orders.Add(order);
                    }
                    return Json(orders);
                }
                catch(SqlException ex) { return Json(Constants.ERROR_CONNECTION_DATABSE); }
                finally { connection.Close(); }
            }
        }



        /// <summary>
        /// Método para obtener los productos de un pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getProductsOrder/{id}")]
        [HttpPost]
        public IHttpActionResult getProducts(int id)
        {
            List<ProductoPedido> products = new List<ProductoPedido>();
            using (SqlConnection connection = DataBase.getConnection())
            {
                SqlCommand command = new SqlCommand("dbo.obtenerProductoPedido", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@idPedido", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoPedido product = new ProductoPedido();
                        product._nombre = reader.GetString(0);
                        product._precio = reader.GetDecimal(1);
                        product._cantidad = reader.GetInt32(2);
                        products.Add(product);
                    }
                    return Json(products);
                }
                catch(SqlException ex) { return Json(Constants.ERROR_CONNECTION_DATABSE); }
                finally { connection.Close(); }
            }
        }


    }
}
