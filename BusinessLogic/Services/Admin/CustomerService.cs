using Restaurant.DataAccess.Repositories;
using System;
using System.ComponentModel;

namespace Restaurant.BusinessLogic.Services
{
    internal class CustomerService : INotifyPropertyChanged
    {
        private string _customerName;
        private int _purchasesCount;

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly AdminRepository _adminRepository;

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

        public int PurchasesCount
        {
            get { return _purchasesCount; }
            set
            {
                if (_purchasesCount != value)
                {
                    _purchasesCount = value;
                    OnPropertyChanged(nameof(PurchasesCount));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CustomerService()
        {
            _adminRepository = new AdminRepository();
        }

        public void BuscarPorTelefono(string telefono)
        {
            try
            {
                var resultado = _adminRepository.ObtenerComprasPorTelefono(telefono);

                if (resultado != null)
                {
                    CustomerName = resultado.Nombre;
                    PurchasesCount = resultado.Compras;
                }
                else
                {
                    CustomerName = "No se encontró usuario";
                    PurchasesCount = 0;
                }
            }
            catch (Exception ex)
            {
                CustomerName = "Error al buscar el usuario";
                PurchasesCount = 0;
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

