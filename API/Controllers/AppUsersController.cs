using API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

[Authorize]
public class AppUsersController : BaseApiController
{
    private readonly DataContext _dbcontext;
    public AppUsersController(DataContext dbContext)
    {
        _dbcontext = dbContext;
    }

    [HttpGet]
    [AllowAnonymous]
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
