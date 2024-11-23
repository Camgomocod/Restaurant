using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Entities
{
    internal class Payment
    {
        public int IdPago { get; set; }
        public int IdPedido { get; set; }
        public int Monto { get; set; }
        public string Metodo { get; set; }
        public string EstadoPago { get; set; }
    }
}
