using Restaurant.DataAccess.Context;
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
using System.Windows.Shapes;
using Restaurant.DataAccess.Repositories;
using Restaurant.BusinessLogic.Services.Login;

namespace Restaurant.Presentation
{
    /// <summary>
    /// Lógica de interacción para LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private readonly AuthService _authService;
        DatabaseContext conn = new DatabaseContext();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void WindowMouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            string correo = txtUser.Text;
            string password = txtPassword.ToString();

            bool isAuthenticated = _authService.Authenticate(correo, password);
            if (isAuthenticated) 
            { 
                MessageBox.Show("¡ Inicio de sesión exitoso", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                var admindForm = new AdminForm();
                admindForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Correo electrónico o contraseña invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TextBlock_Registrarse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
        {
            var registerForm = new RegisterForm();
            registerForm.Show();
        }
    }
}

