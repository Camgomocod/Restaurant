using Restaurant.BusinessLogic.DTOS;
using Restaurant.BusinessLogic.Interfaces;
using Restaurant.DataAccess.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using Oracle.ManagedDataAccess.Client;
using Restaurant.DataAccess.Context;

namespace Restaurant.BusinessLogic.Services
{
    internal class AdminService : IAdminService
    {
        private readonly DatabaseContext _dbContext;
        public AdminService()
        {
            _dbContext = new DatabaseContext();
        }

        /// <summary>
        /// Calcula el subtotal de un pedido sumando los subtotales de sus detalles.
        /// </summary>
        /// <param name="pedidoId">El ID del pedido.</param>
        /// <returns>El subtotal del pedido.</returns>   
        public async Task<decimal> CalcularSubtotal(int pedidoId)
        {
            decimal subtotal = 0;
            try
            {
                _dbContext.abrirConexion();
                using (var command = new OracleCommand("AdminPackage.fn_calcular_subtotal", _dbContext.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Parámetro de entrada: ID del pedido
                    command.Parameters.Add("p_pedido_id", OracleDbType.Int32).Value = pedidoId;

                    // Parámetro de salida: Subtotal calculado
                    command.Parameters.Add("v_subtotal", OracleDbType.Decimal).Direction = System.Data.ParameterDirection.Output;

                    // Ejecutar el comando
                    await command.ExecuteNonQueryAsync();

                    // Obtener el valor de salida
                    subtotal = Convert.ToDecimal(command.Parameters["v_subtotal"].Value.ToString());
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al calcular el subtotal: {ex.Message}");
                throw;
            }
            finally
            {
                _dbContext.cerrarConexion();
            }
            return subtotal;
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
