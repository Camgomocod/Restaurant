using System;
using System.Windows;
using System.Windows.Input;
using Restaurant.DataAccess.Repositories;
using Restaurant.BusinessLogic.Services.Login;
using Restaurant.DataAccess.Entities;
using Restaurant.Presentation.View.User;

namespace Restaurant.Presentation
{
    /// <summary>
    /// Lógica de interacción para LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private readonly AuthService _authService;

        public LoginForm() : this(new AuthService(new UserRepository())) { }

        internal LoginForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
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

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var usuario = new User
                {
                    CorreoElectronico = txtUser.Text,
                    Contrasena = txtPassword.Password,
                };

                bool isAuth = _authService.Authenticate(usuario);
                if (isAuth)
                {
                    string userType = _authService.user_type(usuario);
                    switch (userType)
                    {
                        case "administrador":
                            var adminForm = new AdminForm();
                            MessageBox.Show($"Bienvenido, {usuario.CorreoElectronico}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            adminForm.Show();
                            break;

                        case "cliente":
                            Categories viewCategories = new Categories();
                            MessageBox.Show($"Bienvenido, {usuario.CorreoElectronico}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            viewCategories.Show();
                            this.Close();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show($"Error al inciar sesión intente de nuevo, {usuario.CorreoElectronico}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
        }

        private void TextBlock_Registrarse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
        {
            var registerForm = new RegisterForm();
            registerForm.Show();
            this.Close();
        }
    }
}

