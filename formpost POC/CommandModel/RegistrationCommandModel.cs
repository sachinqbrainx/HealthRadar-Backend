using System.ComponentModel.DataAnnotations;

namespace UserManagement.CommandModel
{
    public class RegistrationCommandModel
    {
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

    }
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class TokenModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

    }
}
