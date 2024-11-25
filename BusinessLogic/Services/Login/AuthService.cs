using Restaurant.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DataAccess.Repositories;
using Restaurant.DataAccess.Entities;
using System.Net.Http.Headers;

namespace Restaurant.BusinessLogic.Services.Login
{
    internal class AuthService : UserRepository
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Authenticate(User user)
        {
            try
            {
                _userRepository.ValidarCredenciales(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string user_type(User user)
        {
            if (user.Rol == "cliente")
            {
                return "cliente";
            }
            else
            {
                return "administrador";
            }
        }

        public bool RegisterUser(User user)
        {
            try
            {
                _userRepository.InsertarUsuario(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
