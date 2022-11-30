
using UserManagement.CommandModel;
using UserManagement.Commands.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace UserManagement.Commands.Handler
{
    public class TokenCommand : ITokenCommand
    {
        private readonly SymmetricSecurityKey key;

        public TokenCommand(IConfiguration config)
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }


        public string CreateToken(RegistrationCommandModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
