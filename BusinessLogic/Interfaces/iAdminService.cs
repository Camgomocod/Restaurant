using Restaurant.BusinessLogic.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Restaurant.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        Task<List<ReporteSemanalDTO>> ObtenerReportesSemanales(DateTime fechaInicio, DateTime fechaFin);
        Task<decimal> CalcularSubtotal(int idPedido);
    }
}
