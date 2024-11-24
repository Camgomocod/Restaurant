namespace Restaurant.DataAccess.Entities
{
    internal class Dish
    {
        public int IdPlato { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Disponibilidad { get; set; }
    }
}
