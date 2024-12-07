using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Restaurant.DataAccess.Context;
using Restaurant.DataAccess.Entities;

namespace Restaurant.DataAccess.Repositories
{
    internal class UserRepository
    {
        private readonly DatabaseContext conn;

        public UserRepository()
        {
            conn = new DatabaseContext();
        }

        #region Implementacion de metodos (Paquete Usuarios)
        public void InsertarUsuario(User usuario)
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

        public void ActualizarUsuario(User usuario)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "pkg_Usuarios.actualizar_usuario";
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

        public void ValidarCredenciales(User user)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "pkg_Usuarios.validar_usuario";
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    command.Parameters.Add("p_correo_electronico", OracleDbType.Varchar2).Value = user.CorreoElectronico;
                    command.Parameters.Add("p_contrasena", OracleDbType.Varchar2).Value = user.Contrasena;

                    // Parámetro de salida
                    command.Parameters.Add("p_resultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    // Ejecutar el comando para validar que el usuario existe
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception("Usuario o contraseña incorrectos.");
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                throw new Exception($"Error al validar credenciales: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }

        public String obtenerRol(String correo)
        {
            try
            {
                conn.abrirConexion();

                using (var command = conn.GetConnection().CreateCommand())
                {
                    command.CommandText = "SELECT pkg_usuarios.obtener_usuario_correo(:p_correo) FROM DUAL";
                    command.Parameters.Add(new OracleParameter("p_correo_electronico", correo));

                    object result = command.ExecuteScalar();
                    if(result != null)
                    {
                        String rol = result.ToString();
                        return rol;
                    }
                    return null;
                }
            }
            catch (OracleException ex)
            {
                throw new Exception($"Error al validar credenciales: {ex.Message}", ex);
            }
            finally
            {
                conn.cerrarConexion();
            }
        }

        #endregion
    }
}
