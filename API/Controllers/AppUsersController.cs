using System.Security.Claims;
using API.Controllers;
using API.Data;
using API.DTO;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API;

[Authorize]
public class AppUsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoservice;

    public AppUsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _photoservice = photoService;
    }

    [HttpGet("users")]
    public async Task<IEnumerable<MemberDto>> GetUsers()
    {
        return await _userRepository.GetMembersAsync();
    }

    [HttpGet("user/{username}")]
    public async Task<MemberDto> GetUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut("user/{id}")]
    public async Task<ActionResult> UpdateUser(int id, MemberUpdateDto memberDto)
    {

        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());
        _mapper.Map(memberDto, user);

        if (await _userRepository.SaveChangesAsync()) return NoContent();
        return BadRequest("Failed to Update User");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());

        if (user == null) return NotFound();

        var result = await _photoservice.AddPhotoAsync(file);
        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            IsMain = (user.Photos.Count() == 0)
        };

        user.Photos.Add(photo);

        if (await _userRepository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetUser), new { username = User.GetUserName() }, _mapper.Map<PhotoDto>(photo));
        }

        return BadRequest("Problem In adding Photo");
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());
        if(user == null) return NotFound();
        var photo = user.Photos.FirstOrDefault(a => a.Id == photoId);
        if(photo.IsMain) return BadRequest("Already the selected Photo is main Photo");

        var currentMain = user.Photos.FirstOrDefault(a => a.IsMain);
        if(currentMain !=null) currentMain.IsMain = false;
        photo.IsMain = true;
        if(await _userRepository.SaveChangesAsync()) return NoContent();

        return BadRequest("Something wrong while update main");
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());
        if(user !=null)
        {
            var photo = user.Photos.FirstOrDefault(a => a.Id == photoId);
            if(photo == null) return NotFound();
            if(photo.IsMain) return BadRequest("You cant delete your main photo");
            if(photo.PublicId !=null)
            {
                var result = await _photoservice.DeletePhotoAsync(photo.PublicId);
                if(result.Error !=null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);
            if(await _userRepository.SaveChangesAsync()) return Ok();

            return BadRequest("Something wrong while delete photo");
        }
        else
        {
            return NotFound("User Not found");
        }
    }
}
