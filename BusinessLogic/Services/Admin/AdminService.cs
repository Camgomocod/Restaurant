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
        #region Attributes
        private readonly DatabaseContext _dbContext;
        private readonly AdminRepository _adminRepository;
        #endregion
        #region Operations
        #region Connection
        public AdminService()
        {
            _dbContext = new DatabaseContext();
        }
        #endregion
        #region CalcularSubTotal
        /// <summary>
        /// Calcula el subtotal de un pedido sumando los subtotales de sus detalles.
        /// </summary>
        /// <param name="pedidoId">El ID del pedido.</param>
        /// <returns>El subtotal del pedido.</returns>   
        /// 
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
        #endregion
        #region ReporteSemanal
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
        #endregion
        #region ActualizarEstadoPedido
        /// <summary>
        /// Actualiza el estado de un pedido.
        /// </summary>
        /// <param name="idPedido">ID del pedido.</param>
        /// <param name="nuevoEstado">Nuevo estado del pedido.</param>
        /// <returns>Una tarea completada si tiene éxito.</returns>
        public async Task ActualizarEstadoPedido(int idPedido, string nuevoEstado)
        {
            try
            {
                _dbContext.abrirConexion();
                using (var command = new OracleCommand("AdminPackage.sp_actualizar_estado_pedido", _dbContext.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add("p_id_pedido", OracleDbType.Int32).Value = idPedido;
                    command.Parameters.Add("p_nuevo_estado", OracleDbType.Varchar2).Value = nuevoEstado;

                    // Ejecutar el procedimiento almacenado
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al actualizar el estado del pedido: {ex.Message}");
                throw;
            }
            finally
            {
                _dbContext.cerrarConexion();
            }
        }
        #endregion
        #region Insertar pago
        /// <summary>
        /// Inserta un nuevo pago asociado a un pedido.
        /// </summary>
        /// <param name="idPedido">ID del pedido asociado.</param>
        /// <param name="monto">Monto del pago.</param>
        /// <param name="metodo">Método de pago (por ejemplo, tarjeta, efectivo).</param>
        /// <param name="estadoPago">Estado del pago (por ejemplo, completado, pendiente).</param>
        /// <returns>Una tarea completada si tiene éxito.</returns>
        public async Task InsertarPago(int idPedido, decimal monto, string metodo, string estadoPago)
        {
            try
            {
                _dbContext.abrirConexion();
                using (var command = new OracleCommand("AdminPackage.sp_insertar_pago", _dbContext.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add("p_id_pedido", OracleDbType.Int32).Value = idPedido;
                    command.Parameters.Add("p_monto", OracleDbType.Decimal).Value = monto;
                    command.Parameters.Add("p_metodo", OracleDbType.Varchar2).Value = metodo;
                    command.Parameters.Add("p_estado_pago", OracleDbType.Varchar2).Value = estadoPago;

                    // Ejecutar el procedimiento almacenado
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al insertar el pago: {ex.Message}");
                throw;
            }
            finally
            {
                _dbContext.cerrarConexion();
            }
        }
        #endregion
        #region ReporteMensual
        /// <summary>
        /// Obtiene los reportes mensuales en base a la fecha proporcionada.
        /// </summary>
        /// <param name="mes">Mes para el cual se requieren los reportes.</param>
        /// <returns>Lista de objetos ReporteMensualDTO.</returns>
        public async Task<List<ReporteMensualDTO>> ObtenerReportesMensuales(DateTime mes)
        {
            return await _adminRepository.ObtenerReportesMensuales(mes);
        }
        #endregion
        #region ConsultarPedidosPorUsuario
        public async Task<List<PedidoDTO>> ConsultarPedidosPorUsuario(int idUsuario)
        {
            return await _adminRepository.ConsultarPedidosPorUsuario(idUsuario);
        } 
        #endregion
        #region ObtenerPedidosPendientes
        public async Task<List<PedidoDTO>> ObtenerPedidosPendientes()
        {
            return await _adminRepository.ObtenerPedidosPendientes();
        } 
        #endregion
        #region ObtenerDetallesPedido
        /// <summary>
        /// Obtiene los detalles de un pedido por su ID.
        /// </summary>
        /// <param name="idPedido">ID del pedido.</param>
        /// <returns>Lista de objetos DetallePedidoDTO.</returns>
        public async Task<List<DetallePedidoDTO>> ObtenerDetallesPedido(int idPedido)
        {
            return await _adminRepository.ObtenerDetallesPedido(idPedido);
        } 
        #endregion
        #endregion
    }

}
