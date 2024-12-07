using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Entities
{
    public class ReporteMensual
    {
        public string Nombre { get; set; }
        public int CantidadPedidos { get; set; }
        public decimal TotalVentas { get; set; }
    }

}
