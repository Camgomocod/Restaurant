using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Restaurant.DataAccess.Context;
using Restaurant.DataAccess.Entities;

namespace Restaurant.DataAccess.Repositories
{
    internal class AdminRepository
    {
        private readonly DatabaseContext conn;

        public AdminRepository()
        {
            conn = new DatabaseContext();
        }

        // Obtener el número de compras realizadas por un usuario, dado su teléfono
        public CustomerPurchases ObtenerComprasPorTelefono(string telefono)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "admin_pkg.obtener_compras_por_telefono";
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    command.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = telefono;

                    // Parámetro de salida para el REF CURSOR
                    var resultadoCursor = new OracleParameter("p_resultado", OracleDbType.RefCursor);
                    resultadoCursor.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultadoCursor);

                    // Ejecutar el comando
                    command.ExecuteNonQuery();

                    // Leer el REF CURSOR
                    using (var reader = ((OracleRefCursor)resultadoCursor.Value).GetDataReader())
                    {
                        if (reader.Read())
                        {
                            return new CustomerPurchases
                            {
                                Nombre = reader.GetString(0),
                                Compras = reader.GetInt32(1)
                            };
                        }
                        else
                        {
                            return new CustomerPurchases
                            {
                                Nombre = "No se encontró usuario",
                                Compras = 0
                            };
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                throw new Exception($"Error al obtener compras por teléfono: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }

        // Generar reporte semanal de pedidos
        public DataTable ReporteSemanal(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "admin_pkg.reporte_semanal";
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add("p_fecha_inicio", OracleDbType.Date).Value = fechaInicio;
                    command.Parameters.Add("p_fecha_fin", OracleDbType.Date).Value = fechaFin;

                    // Parámetro de salida (para obtener los resultados de la consulta)
                    var dataParam = new OracleParameter("p_resultados", OracleDbType.RefCursor);
                    dataParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(dataParam);

                    // Ejecutar el comando
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable result = new DataTable();
                        result.Load(reader);  // Cargar los resultados en un DataTable
                        return result;
                    }
                }
            }
            catch (OracleException ex)
            {
                throw new Exception($"Error al generar el reporte semanal: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }

        // Generar reporte mensual de pedidos
        public DataTable ReporteMensual(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "admin_pkg.reporte_mensual";
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add("p_fecha_inicio", OracleDbType.Date).Value = fechaInicio;
                    command.Parameters.Add("p_fecha_fin", OracleDbType.Date).Value = fechaFin;

                    // Parámetro de salida (para obtener los resultados de la consulta)
                    var dataParam = new OracleParameter("p_resultados", OracleDbType.RefCursor);
                    dataParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(dataParam);

                    // Ejecutar el comando
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable result = new DataTable();
                        result.Load(reader);  // Cargar los resultados en un DataTable
                        return result;
                    }
                }
            }
            catch (OracleException ex)
            {
                throw new Exception($"Error al generar el reporte mensual: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }

        public List<Order> ObtenerTodosLosPedidos()
        {
            try
            {
                conn.abrirConexion();
                List<Order> pedidos = new List<Order>();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "admin_pkg.obtener_lista_pedidos";
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de salida
                    var dataParam = new OracleParameter("p_resultados", OracleDbType.RefCursor);
                    dataParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(dataParam);

                    // Ejecutar el comando
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pedido = new Order
                            {
                                IdOrder = reader.GetInt32(reader.GetOrdinal("id_order")),
                                NameClient = reader.GetString(reader.GetOrdinal("name_client")),
                                DateOrder = reader.GetDateTime(reader.GetOrdinal("date_order")),
                                PayMethod = reader.GetString(reader.GetOrdinal("pay_method")),
                                AsyncState = reader.GetString(reader.GetOrdinal("async_state")),
                                Total = reader.GetDecimal(reader.GetOrdinal("total"))
                            };

                            pedidos.Add(pedido);
                        }
                    }
                }

                return pedidos;
            }
            catch (OracleException ex)
            {
                throw new Exception($"Error al obtener todos los pedidos: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }

    }
}

