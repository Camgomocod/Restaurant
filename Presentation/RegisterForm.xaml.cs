using Restaurant.BusinessLogic.Services.Login;
using Restaurant.DataAccess.Entities;
using Restaurant.DataAccess.Repositories;
using System;
using System.Windows;
using System.Windows.Input;

namespace Restaurant.Presentation
{
    /// <summary>
    /// Lógica de interacción para RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        private readonly AuthService _authService;

        public RegisterForm(): this(new AuthService(new UserRepository())) { }
        internal RegisterForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService ?? throw new ArgumentNullException(nameof(AuthService));
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Password) ||
                    string.IsNullOrWhiteSpace(txtDirection.Text) ||
                    string.IsNullOrWhiteSpace(txtCellNumber.Text)) 
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var user = new User
                {
                    Nombre = txtName.Text,
                    CorreoElectronico = txtCorreo.Text,
                    Contrasena = txtPassword.Password,
                    Direccion = txtDirection.Text,
                    Telefono = txtCellNumber.Text,
                };

                bool isAuth = _authService.RegisterUser(user);
                if (isAuth)
                {
                    MessageBox.Show($"Usuario registrado exitosamente, {user.Nombre}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Error al registrar el usuario {user.Nombre}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WindowMouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // Hay que ver que no se cierren las dos ventanas cuando se presiona el botón
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var loginForm = new LoginForm();
            this.Close();
            loginForm.Show();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
