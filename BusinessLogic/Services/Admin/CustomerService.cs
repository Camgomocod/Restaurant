using Restaurant.DataAccess.Repositories;

namespace Restaurant.BusinessLogic.Services
{
    internal class CustomerService : UserRepository
    {
        // Esta pendiente hacer la conexión con el data acces para obtener esos datos y ser usados aqui
        private readonly UserRepository _userRepository;
        public string CustomerName { get; set; }

        public CustomerService()
        {
        }
    }

}
