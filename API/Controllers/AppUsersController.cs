using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API;

[ApiController]
[Route("api/[controller]")]
public class AppUsersController : ControllerBase
{
    private readonly DataContext _dbcontext;
    public AppUsersController(DataContext dbContext)
    {
        _dbcontext = dbContext;
    }

    [HttpGet]
    public async Task<IEnumerable<AppUser>> GetUsers()
    {
        var Users = await _dbcontext.AppUsers.ToListAsync();

        return Users;
    }

    [HttpGet("{id}")]
    public async Task<AppUser> GetUserById(int id)
    {
        var user = await _dbcontext.AppUsers.Where(a => a.Id == id).FirstOrDefaultAsync();
        return user;
    }

    [HttpPost]
    public async Task<AppUser> CreateUser(AppUser appUser)
    {
        await _dbcontext.AppUsers.AddAsync(appUser);
        return appUser;
    }

     [HttpPut]
    public async Task<AppUser> UpdateUser(int id,AppUser appUser)
    {
        var dbAppUser = _dbcontext.AppUsers.Where(a => a.Id == id).FirstOrDefault();
        
        return appUser;
    }

}
