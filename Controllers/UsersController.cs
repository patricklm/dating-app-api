using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]

public class UsersController(IUserRepository userRepo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepo.GetMembersAsync();
        return Ok(users);
    }


    [HttpGet("{username}")]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(string username)
    {
        var user = await userRepo.GetMemberAsync(username);
        if (user == null) return NotFound();

        return Ok(user);
    }
}
