﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPATEC_Service_2.Models
{
    public class ProductoPedido
    {

        public int _id { get; set; }
        public string _nombre { get; set; }
        public decimal _precio { get; set; }
        public int _cantidad { get; set; }
    }
}