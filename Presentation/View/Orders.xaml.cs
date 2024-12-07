using Restaurant.BusinessLogic.Services;
using Restaurant.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant.Presentation.View
{
    /// <summary>
    /// Lógica de interacción para Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        private readonly OrdersService _ordersService;

        public Orders()
        {
            InitializeComponent();
            _ordersService = new OrdersService();  // Instanciar el servicio
            DataContext = _ordersService;  // Asignar el DataContext al servicio
        }

        private void BtnCargarPedidos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener la lista de pedidos
                List<Order> pedidos = _ordersService.ObtenerTodosLosPedidos();

                if (pedidos.Count == 0)
                {
                    MessageBox.Show("No se encontraron pedidos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Asignar la lista de pedidos al DataGrid
                    dataGridPedidos.ItemsSource = pedidos;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al cargar los pedidos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
