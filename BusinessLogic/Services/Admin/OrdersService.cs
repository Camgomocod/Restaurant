using System.Collections.Generic;
using Restaurant.DataAccess.Repositories;
using Restaurant.DataAccess.Entities;

namespace Restaurant.BusinessLogic.Services
{
    public class OrdersService
    {
        private readonly AdminRepository _adminRepository;

        public OrdersService()
        {
            _adminRepository = new AdminRepository(); // Instanciamos el repositorio
        }

        public List<Order> ObtenerTodosLosPedidos()
        {
            return _adminRepository.ObtenerTodosLosPedidos();
        }
    }
}
