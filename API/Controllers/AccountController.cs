using System.Security.Cryptography;
using System.Text;
using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _dbcontext;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext dbContext,ITokenService tokenService)
        {
            _dbcontext = dbContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (await this.UserExists(registerDto.username))
                {
                    return BadRequest($"Username {registerDto.username} already taken");
                }
                else
                {
                    using var hmac = new HMACSHA512();
                    var user = new AppUser
                    {
                        UserName = registerDto.username,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                        PasswordSalt = hmac.Key
                    };

                    _dbcontext.Add(user);
                    await _dbcontext.SaveChangesAsync();

                    return user;
                }
            }
            else
            {
                return BadRequest("Input Parameters are not valid");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>?> Login(LoginDto loginDto)
        {
            var dbuser =await _dbcontext.AppUsers.FirstOrDefaultAsync(a => a.UserName == loginDto.username);
            if (dbuser != null)
            {
                using var hmac = new HMACSHA512(dbuser.PasswordSalt);
                if (dbuser.PasswordHash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password))))
                {
                    return new LoginResponseDto
                    {
                        token = _tokenService.CreateToken(dbuser),
                        username = dbuser.UserName
                    } ;
                }
                else
                {
                    return Unauthorized("Invalid Password");
                }
            }

            return Unauthorized("Invalid Username or Password");
        }

        private async Task<bool> UserExists(string username)
        {
            return await _dbcontext.AppUsers.AnyAsync(a => a.UserName == username);
        }
    }
}