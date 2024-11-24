using System;

namespace Restaurant.BusinessLogic.DTOS
{
    public class ReporteMensualDTO 
    {
        public int IdPedido { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
    }

}
