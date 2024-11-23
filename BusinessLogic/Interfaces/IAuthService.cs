namespace Restaurant.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        // Implementar la funcionalidad de hacer la distinción entre usuarios 
        bool Login(string username, string password);
        void Logout();
        bool IsAuthenticade();
    }
}
