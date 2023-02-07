
using Autentication.Core.DTO;

namespace Autentication.Core.Interfaces
{
    public interface IAccountService
    {
        Task SignUp(string username, string password);
        Task<JsonWebToken> SignIn(string username, string password);
    }
}