namespace Restaurant.DataAccess.Entities
{
    internal class User
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
    }
}
