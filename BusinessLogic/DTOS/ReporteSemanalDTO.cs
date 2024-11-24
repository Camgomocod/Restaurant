using System;

namespace Restaurant.BusinessLogic.DTOS
{
    public class ReporteSemanalDTO 
    {
        public DateTime SemanaInicio { get; set; }
        public decimal TotalVentas { get; set; }
        public int TotalPedidos { get; set; }
        public int PedidosCompletados { get; set; }
        public int PedidosCancelados { get; set; }
    }

}
