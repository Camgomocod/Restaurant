using System;
using System.Windows;
using System.Windows.Input;
using Restaurant.Presentation.View;

namespace Restaurant.Presentation
{
    /// <summary>
    /// Lógica de interacción para AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        public AdminForm()
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

        public void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new Home();
        }

        public void CustomerButton_Click(Object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new Customers();
        }

        public void DishesButton_Click(Object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new Dishes();
        }

        public void ReportButton_Click(Object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new Reports();
        }

        public void OrderButton_Click(Object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new Orders();
        }

        public void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.Show();
            this.Close();
        }
    }
}
