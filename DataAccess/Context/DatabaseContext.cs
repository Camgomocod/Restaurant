using System;
using Oracle.ManagedDataAccess.Client;

namespace Restaurant.DataAccess.Context
{
    public class DatabaseContext
    {
        // Estos valores deberan ser cambiados segun la estructura
        // de la base de datos de la maquina local
        #region Attributes
        private string connectionString;
        private string usuario = "C##develop";
        private string contraseña = "oracle";
        private string host = "localhost";
        private int puerto = 1521;
        private string servicio = "xe";
        public OracleConnection conn; 
        #endregion

        public DatabaseContext()
        {
            //Constuir cadena de conexion una vez
            connectionString = $"User Id={usuario};Password={contraseña};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={puerto}))(CONNECT_DATA=(SID={servicio})));";
            conn = new OracleConnection(connectionString);
        }

        // Metodo para abrir conexion
        public void abrirConexion()
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                throw; // Rethrow para manejarlo en niveles superiores si es necesario
            }
        }

        // Método para cerrar la conexión
        public void cerrarConexion()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        // Metodo get
        public OracleConnection GetConnection()
        {
            return conn;
        }

        // Validacion de conexion
        public void validarConexion()
        {
            try
            {
                conn.Open();
                Console.WriteLine("Conexión exitosa con la base de datos Oracle.");
            }
            catch (OracleException ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
        }
    }
}
