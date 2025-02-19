using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]

public class UsersController(DataContext context) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
}
