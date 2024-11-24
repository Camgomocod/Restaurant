using System;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Restaurant.DataAccess.Context;

namespace Restaurant.DataAccess.Repositories
{
    public class AdminRepository
    {
        protected readonly DatabaseContext conn;

        public AdminRepository()
        {
            conn = new DatabaseContext();
        }
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

    }
}
