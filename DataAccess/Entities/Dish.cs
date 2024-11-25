namespace Restaurant.DataAccess.Entities
{
    public class Dish
    {
        public int IdPlato { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Disponibilidad { get; set; }
        public string Imagen { get; set; }
    }
}
