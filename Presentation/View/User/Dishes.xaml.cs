using Restaurant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant.Presentation.View.User
{
    /// <summary>
    /// Lógica de interacción para Dishes.xaml
    /// </summary>
    public partial class Dishes : Window
    {
        public ObservableCollection<Dish> Platos { get; set; }
        public Dishes()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None; // Oculta el encabezado
            this.ResizeMode = ResizeMode.NoResize; // Deshabilita el cambio de tamaño
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = this;
        }

        public Dishes build(int IdCategoriaPlato)
        {
            //TODO: se obtiene los platos por id categoria plato
            Platos = new ObservableCollection<Dish>
            {
                new Dish
                {
                    Nombre = "Almuerzo Ejecutivo",
                    Descripcion = "Comida balanceada y sofisticada durante su jornada laboral...",
                    Imagen = "/Presentation/Resources/Images/User/Categories/Almuerzo_infantil.png"
                },
                new Dish
                {
                    Nombre = "Almuerzo Infantil",
                    Descripcion = "Porciones adecuadas y opciones deliciosas...",
                    Imagen = "/Presentation/Resources/Images/User/Categories/Almuerzo_infantil.png"
                },
                new Dish
                {
                    Nombre = "Comida Rápida",
                    Descripcion = "Opciones rápidas y deliciosas para disfrutar con amigos...",
                    Imagen = "/Presentation/Resources/Images/User/Categories/Bebidas.png"
                },
                new Dish
                {
                    Nombre = "Bebidas",
                    Descripcion = "Refresca tu experiencia con nuestra variedad de bebidas...",
                    Imagen = "/Presentation/Resources/Images/User/Categories/Comidas_Rapidas.png"
                }
            };

            // Asignar la lista como fuente de datos
            
            return new Dishes();
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que fue clickeado
            var button = sender as Button;

            // Obtener el contexto de datos del botón (es decir, el ítem de la categoría)
            var Dish = button?.DataContext as Dish;

            // Verificar que la categoría no sea null y mostrar un MessageBox con su nombre
            if (Dish != null)
            {
                MessageBox.Show($"Has seleccionado la categoría: {Dish.Nombre}", "Categoría Seleccionada", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar el cuadro de diálogo de confirmación
            MessageBoxResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas cerrar sesión?",
                "Confirmar cierre de sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            // Verificar la respuesta del usuario
            if (resultado == MessageBoxResult.Yes)
            {
                // Redirigir a la vista de inicio de sesión
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                // Cerrar la ventana actual (ventana principal)
                this.Close();
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
