using System.Security.Cryptography;
using System.Text;
using API.DTO;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _dbcontext;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;
        public AccountController(DataContext dbContext, ITokenService tokenService, IMapper mapper)
        {
            this.mapper = mapper;
            _dbcontext = dbContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (await this.UserExists(registerDto.UserName))
                {
                    return BadRequest($"Username {registerDto.UserName} already taken");
                }
                else
                {
                    var user = mapper.Map<AppUser>(registerDto);
                    using var hmac = new HMACSHA512();

                    user.UserName = registerDto.UserName.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
                    user.PasswordSalt = hmac.Key;
                    user.Interests = "";
                    user.Introduction = "";
                    user.LookingFor = "";

                    _dbcontext.Add(user);
                    await _dbcontext.SaveChangesAsync();

                    return new UserDto
                    {
                        UserName = user.UserName,
                        KnownAs = user.KnownAs,
                        PhotoUrl = user.Photos.FirstOrDefault(a => a.IsMain)?.Url,
                        Token = _tokenService.CreateToken(user)
                    };
                }
            }
            else
            {
                return BadRequest("Input Parameters are not valid");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>?> Login(LoginDto loginDto)
        {
            var dbuser = await _dbcontext.AppUsers.Include(p => p.Photos).FirstOrDefaultAsync(a => a.UserName == loginDto.username);
            if (dbuser != null)
            {
                using var hmac = new HMACSHA512(dbuser.PasswordSalt);
                if (dbuser.PasswordHash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password))))
                {
                    return new UserDto
                    {
                        Token = _tokenService.CreateToken(dbuser),
                        UserName = dbuser.UserName,
                        PhotoUrl = dbuser.Photos.FirstOrDefault(a => a.IsMain)?.Url,
                        KnownAs = dbuser.KnownAs
                    };
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