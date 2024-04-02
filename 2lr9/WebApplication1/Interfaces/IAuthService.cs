using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IAuthService
    {
        IResult Login(string email, string password);
        IResult Register(User.RegisterUser user);
    }
}
