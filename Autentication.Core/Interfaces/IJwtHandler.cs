using Autentication.Core.DTO;
using Autentication.Core.Models;

namespace Autentication.Core.Interfaces
{
    public interface IJwtHandler
    {
        JsonWebToken GenerateToken(User user);
    }
}