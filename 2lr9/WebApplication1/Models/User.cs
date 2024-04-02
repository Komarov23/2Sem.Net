using System.ComponentModel.DataAnnotations;
using WebApplication1.Services;

namespace WebApplication1.Models
{
    public class User(
        int id, 
        string name, 
        string lastName, 
        string email, 
        string password, 
        DateTime birth, 
        DateTime lastAuth, 
        int failedAuth
        )
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string LastName { get; set; } = lastName;
        [EmailAddress(ErrorMessage = "Невірний формат")]
        public string Email { get; set; } = email;
        public string Password { get; set; } = HashPasswordService.Encrypt(password);
        public DateTime Birth { get; set; } = birth;
        public DateTime LastAuth { get; set; } = lastAuth;
        public int FailedAuth { get; set; } = failedAuth;

        public record class LoginUser(string Email, string Password);
        public record class RegisterUser(string Name, string LastName, [EmailAddress(ErrorMessage = "Невірний формат")] string Email, string Password, DateTime Birth);
    }
}