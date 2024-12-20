﻿using Restaurant.BusinessLogic.Services;
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
    /// Lógica de interacción para Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        public float Number = 200;
        private CustomerService _customerService;

        public Customers()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            this.DataContext = _customerService; // Asignar el servicio como el DataContext
        }

        private void BtnSearchUser_Click(object sender, RoutedEventArgs e)
        {
            var customerService = (CustomerService)DataContext;
            customerService.BuscarPorTelefono(txtName.Text);
        }

    }
}
