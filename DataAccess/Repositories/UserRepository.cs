using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Restaurant.DataAccess.Context;

namespace Restaurant.DataAccess.Repositories
{
    internal class UserRepository
    {
        private readonly DatabaseContext conn;

        public UserRepository()
        {
            conn = new DatabaseContext();
        }

        #region Modelos
        public class Usuario
        {
            public int IdUsuario { get; set; }
            public string Nombre { get; set; }
            public string CorreoElectronico { get; set; }
            public string Contrasena { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
            public string Rol { get; set; }
        }

        public class Pedido
        {
            public int IdPedido { get; set; }
            public int IdUsuario { get; set; }
            public string MetodoPago { get; set; }
            public string Estado { get; set; }
            public decimal Total { get; set; }
        }
        #endregion

        #region Implementacion de metodos (Paquete Usuarios)
        public void InsertarUsuario(Usuario usuario)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "pkg_Usuarios.registrar_usuario";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = usuario.Nombre;
                    command.Parameters.Add("p_correo_electronico", OracleDbType.Varchar2).Value = usuario.CorreoElectronico;
                    command.Parameters.Add("p_contrasena", OracleDbType.Varchar2).Value = usuario.Contrasena;
                    command.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = usuario.Direccion;
                    command.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = usuario.Telefono;

                    command.ExecuteNonQuery();
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 20001)
                    throw new Exception("El correo electrónico ya está registrado.", ex);
                throw new Exception($"Error al insertar usuario: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "pkg_Usuarios.actualizar_usuario";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = usuario.IdUsuario;
                    command.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = usuario.Nombre;
                    command.Parameters.Add("p_correo_electronico", OracleDbType.Varchar2).Value = usuario.CorreoElectronico;
                    command.Parameters.Add("p_contrasena", OracleDbType.Varchar2).Value = usuario.Contrasena;
                    command.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = usuario.Direccion;
                    command.Parameters.Add("p_telefono", OracleDbType.Varchar2).Value = usuario.Telefono;

                    command.ExecuteNonQuery();
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 20003)
                    throw new Exception("El usuario no existe.", ex);
                if (ex.Number == 20004)
                    throw new Exception("No se pudo actualizar el usuario.", ex);
                throw new Exception($"Error al actualizar usuario: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }
        public void CrearPedido(Pedido pedido)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "pkg_Usuarios.crear_pedido";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = pedido.IdUsuario;
                    command.Parameters.Add("p_metodo_pago", OracleDbType.Varchar2).Value = pedido.MetodoPago;
                    command.Parameters.Add("p_estado", OracleDbType.Varchar2).Value = pedido.Estado;
                    command.Parameters.Add("p_total", OracleDbType.Decimal).Value = pedido.Total;

                    command.ExecuteNonQuery();
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 20006)
                    throw new Exception("El usuario especificado no existe.", ex);
                throw new Exception($"Error al crear el pedido: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }
        #endregion
    }
}
