using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Restaurant.BusinessLogic.DTOS;
using Restaurant.DataAccess.Context;

namespace Restaurant.DataAccess.Repositories
{
    public class AdminRepository
    {
        #region Attribute
        protected readonly DatabaseContext conn;
        #endregion
        #region Operations
        #region connection
        public AdminRepository()
        {
            conn = new DatabaseContext();
        }
        #endregion
        #region ObtenerReportesSemanales
        public async Task<DataTable> ObtenerReportesSemanales(DateTime fechaInicio, DateTime fechaFin)
        {
            {
                try
                {
                    conn.abrirConexion();
                    using (var command = new OracleCommand("AdminPackage.fn_obtener_reportes_semanales", conn.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros
                        command.Parameters.Add("p_fecha_inicio", OracleDbType.Date).Value = fechaInicio;
                        command.Parameters.Add("p_fecha_fin", OracleDbType.Date).Value = fechaFin;
                        command.Parameters.Add("v_reporte", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        // Ejecutar y cargar los resultados
                        var adapter = new OracleDataAdapter(command);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine($"Error en ObtenerReportesSemanales: {ex.Message}");
                    throw;
                }
                finally
                {
                    conn.cerrarConexion();
                }
            }
        }
        #endregion
        #region ObtenerReportesMensuales
        /// <summary>
        /// Obtiene los reportes mensuales en base a la fecha proporcionada.
        /// </summary>
        /// <param name="mes">Mes para el cual se requieren los reportes.</param>
        /// <returns>Lista de objetos ReporteMensualDTO.</returns>
        public async Task<List<ReporteMensualDTO>> ObtenerReportesMensuales(DateTime mes)
        {
            var reportes = new List<ReporteMensualDTO>();
            try
            {
                conn.abrirConexion();

                using (var command = new OracleCommand("AdminPackage.fn_obtener_reportes_mensuales", conn.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add("p_mes", OracleDbType.Date).Value = mes;

                    // Parámetro de salida (SYS_REFCURSOR)
                    command.Parameters.Add("v_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;

                    // Ejecutar el comando
                    await command.ExecuteNonQueryAsync();

                    // Leer el cursor de resultados
                    using (var reader = ((OracleRefCursor)command.Parameters["v_cursor"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            reportes.Add(new ReporteMensualDTO
                            {
                                IdPedido = reader.GetInt32(0),
                                Cliente = reader.GetString(1),
                                Total = reader.GetDecimal(2),
                                FechaPedido = reader.GetDateTime(3),
                                Estado = reader.GetString(4)
                            });
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al obtener reportes mensuales: {ex.Message}");
                throw;
            }
            finally
            {
                conn.cerrarConexion();
            }

            return reportes;
        }
        #endregion
        #region CalcularSubtotal
        public async Task<decimal> CalcularSubtotal(int idPedido)
        {
            {
                try
                {
                    conn.abrirConexion();
                    using (var command = new OracleCommand("AdminPackage.fn_calcular_subtotal", conn.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros
                        command.Parameters.Add("p_id_pedido", OracleDbType.Int32).Value = idPedido;
                        command.Parameters.Add("v_subtotal", OracleDbType.Decimal).Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el procedimiento
                        await command.ExecuteNonQueryAsync();

                        // Retornar el valor del subtotal
                        return Convert.ToDecimal(command.Parameters["v_subtotal"].Value);
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine($"Error en CalcularSubtotal: {ex.Message}");
                    throw;
                }
                finally
                {
                    conn.cerrarConexion();
                }
            }
        }
        #endregion
        #region ActualizarEstadoPedido
        public async Task ActualizarEstadoPedido(int idPedido, string nuevoEstado)
        {
            {
                try
                {
                    conn.abrirConexion();
                    using (var command = new OracleCommand("AdminPackage.sp_actualizar_estado_pedido", conn.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros
                        command.Parameters.Add("p_id_pedido", OracleDbType.Int32).Value = idPedido;
                        command.Parameters.Add("p_nuevo_estado", OracleDbType.Varchar2).Value = nuevoEstado;

                        // Ejecutar el procedimiento
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine($"Error en ActualizarEstadoPedido: {ex.Message}");
                    throw;
                }
                finally
                {
                    conn.cerrarConexion();
                }
            }
        }
        #endregion
        #region InsertarPago
        public async Task InsertarPago(int idPedido, decimal monto, string metodo, string estadoPago)
        {
            {
                try
                {
                    conn.abrirConexion();
                    using (var command = new OracleCommand("AdminPackage.sp_insertar_pago", conn.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros
                        command.Parameters.Add("p_id_pedido", OracleDbType.Int32).Value = idPedido;
                        command.Parameters.Add("p_monto", OracleDbType.Decimal).Value = monto;
                        command.Parameters.Add("p_metodo", OracleDbType.Varchar2).Value = metodo;
                        command.Parameters.Add("p_estado_pago", OracleDbType.Varchar2).Value = estadoPago;

                        // Ejecutar el procedimiento
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine($"Error en InsertarPago: {ex.Message}");
                    throw;
                }
                finally
                {
                    conn.cerrarConexion();
                }
            }
        }
        #endregion
        #region ConsultarPedidosPorUsuario
        public async Task<List<PedidoDTO>> ConsultarPedidosPorUsuario(int idUsuario)
        {
            var pedidosUsuario = new List<PedidoDTO>();
            try
            {
                conn.abrirConexion();

                using (var command = new OracleCommand("AdminPackage.consultar_Pedidos_Por_Usuario", conn.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = idUsuario;

                    // Parámetro de salida (SYS_REFCURSOR)
                    command.Parameters.Add("v_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    // Ejecutar el procedimiento
                    await command.ExecuteNonQueryAsync();

                    // Leer el cursor de resultados
                    using (var reader = ((OracleRefCursor)command.Parameters["v_cursor"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            pedidosUsuario.Add(new PedidoDTO
                            {
                                IdPedido = reader.GetInt32(0),         // Primer campo del cursor
                                FechaPedido = reader.GetDateTime(1),  // Segundo campo
                                Estado = reader.GetString(2),         // Tercer campo
                                Total = reader.GetDecimal(3)          // Cuarto campo
                            });
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al consultar pedidos por usuario: {ex.Message}");
                throw;
            }
            finally
            {
                conn.cerrarConexion();
            }

            return pedidosUsuario;
        } 
        #endregion
        #region ObtenerPedidosPendientes
        public async Task<List<PedidoDTO>> ObtenerPedidosPendientes()
        {
            var pedidosPendientes = new List<PedidoDTO>();
            try
            {
                conn.abrirConexion();

                using (var command = new OracleCommand("AdminPackage.sp_obtener_pedidos_pendientes", conn.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de salida (SYS_REFCURSOR)
                    command.Parameters.Add("v_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    // Ejecutar el procedimiento
                    await command.ExecuteNonQueryAsync();

                    // Leer el cursor de resultados
                    using (var reader = ((OracleRefCursor)command.Parameters["v_cursor"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            pedidosPendientes.Add(new PedidoDTO
                            {
                                IdPedido = reader.GetInt32(0),         // Primer campo del cursor
                                FechaPedido = reader.GetDateTime(1),  // Segundo campo
                                Estado = reader.GetString(2),         // Tercer campo
                                Total = reader.GetDecimal(3)          // Cuarto campo
                            });
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al obtener pedidos pendientes: {ex.Message}");
                throw;
            }
            finally
            {
                conn.cerrarConexion();
            }

            return pedidosPendientes;
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
            var detalles = new List<DetallePedidoDTO>();
            try
            {
                conn.abrirConexion();

                using (var command = new OracleCommand("AdminPackage.obtener_detalles_pedido", conn.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    command.Parameters.Add("p_id_pedido", OracleDbType.Int32).Value = idPedido;

                    // Parámetro de salida (SYS_REFCURSOR)
                    command.Parameters.Add("v_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    // Ejecutar el procedimiento
                    await command.ExecuteNonQueryAsync();

                    // Leer el cursor de resultados
                    using (var reader = ((OracleRefCursor)command.Parameters["v_cursor"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            detalles.Add(new DetallePedidoDTO
                            {
                                IdProducto = reader.GetInt32(0),         // Supongamos que es el primer campo del cursor
                                NombreProducto = reader.GetString(1),   // Segundo campo
                                Cantidad = reader.GetInt32(2),          // Tercer campo
                                PrecioUnitario = reader.GetDecimal(3),  // Cuarto campo
                                Subtotal = reader.GetDecimal(4)         // Quinto campo
                            });
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al obtener los detalles del pedido: {ex.Message}");
                throw;
            }
            finally
            {
                conn.cerrarConexion();
            }

            return detalles;
        }
        #endregion 
        #endregion
    }
}
