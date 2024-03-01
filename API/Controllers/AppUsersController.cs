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
    public AppUsersController(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MemberDto>> GetUsers()
    {
        return await _userRepository.GetMembersAsync();
    }

    [HttpGet("{username}")]
    public async Task<MemberDto> GetUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }
}
