using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPATEC_Service_2.Models
{
    /// <summary>
    /// Clase que representa la tabla PEDIDO de la base de datos
    /// </summary>
    public class Pedido
    {

        public int _id { get; set; }
        public DateTime _fechaHora { get; set; }
        public decimal _total { get; set; }
        public bool _estado { get; set; } 
        public string _cliente { get; set; }
        public string _vendedor { get; set; }
        public int _sucursal { get; set; }
        public string _nombreSucursal { get; set; }
        public List<ProductoPedido> _productos { get; set; }
    }
}