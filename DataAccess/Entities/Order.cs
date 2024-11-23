﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Entities
{
    internal class Order
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        public double Total { get; set; }
        public DateTime FechaPedido { get; set; }
    }
}