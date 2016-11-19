using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPATEC_Service_2.Models
{

    /// <summary>
    /// Clase que representa a la tabla PRODUCTO de la base de datos
    /// </summary>
    public class Producto
    {
        public int _id { get; set; }
        public string _nombre { get; set; }
        public string _descripcion { get; set; }
        public Boolean _exento { get; set; }
        public int _cantDisponible { get; set; }
        public decimal _precio { get; set; }
        public string _categoria { get; set; }
        public string _proveedor { get; set; }
        public string _sucursal { get; set; }
    }
}