
using UserManagement.CommandModel;
using UserManagement.Commands.Interface;
using UserManagement.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace formpost_POC.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase  
    {
        private readonly DataDbContext _context;
        private readonly ITokenCommand _tokenService;

        public LoginController(DataDbContext context, ITokenCommand tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<TokenModel>> login(LoginModel loginDto)
        {
            var user = await _context.UserManagement.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null) return Unauthorized("Invalid UserName");

            if (user.Password != loginDto.Password) return Unauthorized("Invalid Password");

            return new TokenModel
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                Role = user.Role.ToLower(),   
            };
        }



        [HttpPost("register")]
        public async Task<ActionResult<RegistrationCommandModel>> Register(RegistrationCommandModel registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest("UserName Already Taken");

            var user = new RegistrationCommandModel
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Role=registerDto.Role.ToLower(),

            };

            _context.UserManagement.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }


        private async Task<bool> UserExists(string username)
        {
            return await _context.UserManagement.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
