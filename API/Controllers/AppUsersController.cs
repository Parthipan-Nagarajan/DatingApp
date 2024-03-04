using System.Security.Claims;
using API.Controllers;
using API.DTO;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

[Authorize]
public class AppUsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public AppUsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
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
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userRepository.GetUserByUserNameAsync(username);
        _mapper.Map(memberDto, user);

        if (await _userRepository.SaveChangesAsync()) return NoContent();
        return BadRequest("Failed to Update User");
    }
}
