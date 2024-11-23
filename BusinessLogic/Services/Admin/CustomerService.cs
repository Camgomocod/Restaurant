using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
