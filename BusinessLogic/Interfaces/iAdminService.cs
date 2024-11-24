using Restaurant.BusinessLogic.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Restaurant.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        #region ObtenerReportesSemanales
        Task<List<ReporteSemanalDTO>> ObtenerReportesSemanales(DateTime fechaInicio, DateTime fechaFin); 
        #endregion
        #region CalcularSubtotal
        Task<decimal> CalcularSubtotal(int idPedido); 
        #endregion
        #region ObtenerReportesMensuales
        /// <summary>
        /// Obtiene los reportes mensuales en base a la fecha proporcionada.
        /// </summary>
        /// <param name="mes">Mes para el cual se requieren los reportes.</param>
        /// <returns>Lista de objetos ReporteMensualDTO.</returns>
        Task<List<ReporteMensualDTO>> ObtenerReportesMensuales(DateTime mes);
        #endregion
        #region ConsultarPedidosPorUsuario
        Task<List<PedidoDTO>> ConsultarPedidosPorUsuario(int idUsuario); 
        #endregion
        #region ObtenerPedidosPendientes
        Task<List<PedidoDTO>> ObtenerPedidosPendientes(); 
        #endregion
    }
}
