using Restaurant.DataAccess.Repositories;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant.Presentation.View
{
    /// <summary>
    /// Lógica de interacción para Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
        }
        // hola
        private void BtnSemanalReport_Click(object sender, RoutedEventArgs e)
        {
            var fechaInicio = initialDate.SelectedDate.Value;
            var fechaFin = finalDate.SelectedDate.Value;

            var reportData = new AdminRepository().ReporteSemanal(fechaInicio, fechaFin);

            // Asignar el DataTable a tu DataGrid
            ReporteSemanalGrid.ItemsSource = reportData.DefaultView;
        }

        private void BtnMonthReport_Click(object sender, RoutedEventArgs e)
        {
            var selectedMonth = (MesComboBox.SelectedItem as ComboBoxItem).Tag.ToString();
            var fechaInicio = new DateTime(DateTime.Now.Year, int.Parse(selectedMonth), 1);
            var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

            var reportData = new AdminRepository().ReporteMensual(fechaInicio, fechaFin);

            ReporteMensualGrid.ItemsSource = reportData.DefaultView;
        }

    }
}
