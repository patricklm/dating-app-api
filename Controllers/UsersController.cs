using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class UsersController(IUserRepository userRepo, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepo.GetMembersAsync();
        return Ok(users);
    }


    [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
        var user = await userRepo.GetMemberAsync(username);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (username == null)
        {
            return BadRequest("No username found");
        }

        var user = await userRepo.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return BadRequest("No user found");
        }

        mapper.Map(memberUpdateDto, user);

        if (await userRepo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Failed to update the user");
    }
}
