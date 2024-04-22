using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class AuthService(IDB db) : IAuthService
    {
        public IResult Login(string email, string password)
        {
            User? user = db.Users.FirstOrDefault(u => u.Email.Equals(email) 
                && HashPasswordService.ComparePasswords(u.Password, password));

            if (user == null)
            {
                return Results.Unauthorized();
            }

            return JWTService.GenerateJWT(user.Email);

        }

        public IResult Register(User.RegisterUser user)
        {
            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            if (!isValid)
            {
                return Results.BadRequest(validationResults);
            }

            db.Users.Add(new User(
                db.Users.Count + 1,
                user.Name,
                user.LastName,
                user.Email,
                HashPasswordService.Encrypt(user.Password),
                user.Birth,
                new DateTime(),
                0
            ));

            return JWTService.GenerateJWT(user.Email);
        }
    }
}
