using Restaurant.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DataAccess.Repositories;

namespace Restaurant.BusinessLogic.Services.Login
{
    internal class AuthService : UserRepository
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Authenticate(string correo, string password)
        {
            var user = _userRepository.ValidarCredenciales(correo, password);
            return user != null;
        }
    }
}
