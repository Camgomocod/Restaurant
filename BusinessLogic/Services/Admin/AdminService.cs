using Restaurant.BusinessLogic.DTOS;
using Restaurant.BusinessLogic.Interfaces;
using Restaurant.DataAccess.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Restaurant.BusinessLogic.Services
{
    internal class AdminService : IAdminService
    {
        public Task<decimal> CalcularSubtotal(int idPedido)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReporteSemanalDTO>> ObtenerReportesSemanales(DateTime fechaInicio, DateTime fechaFin)
        {
            var adminRepository = new AdminRepository();
            var dataTable = await adminRepository.ObtenerReportesSemanales(fechaInicio, fechaFin);

            var reportes = new List<ReporteSemanalDTO>();

            foreach (DataRow row in dataTable.Rows)
            {
                reportes.Add(new ReporteSemanalDTO
                {
                    SemanaInicio = Convert.ToDateTime(row["semana_inicio"]),
                    TotalVentas = Convert.ToDecimal(row["total_ventas"]),
                    TotalPedidos = Convert.ToInt32(row["total_pedidos"]),
                    PedidosCompletados = Convert.ToInt32(row["pedidos_completados"]),
                    PedidosCancelados = Convert.ToInt32(row["pedidos_cancelados"])
                });
            }
            return reportes;
        }
    }

}
