using API.Data;
using API.DOTs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController(DataContext context) : ControllerBase
    {

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuth()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetAppUser()
        {
            var user = context.Users.Find(-1);
            if (user is null) return NotFound("User not found");
            return user;
        }

        [HttpGet("server-error")]
        public ActionResult<AppUser> GetUserError()
        {
            var user = context.Users.Find(-1)
                ?? throw new Exception("Server error while fetching a user");
            return user;
        }

        [HttpGet("bad-request")]
        public ActionResult<AppUser> GetBadRequest()
        {
            return BadRequest("Bad request thrown on server side");
        }

        [HttpPost("validation-error")]
        public ActionResult GetValidUser([FromBody] RegisterDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            return Ok("Model id valid");

        }

        [HttpGet("null-pointer")]
        public ActionResult<string> GetAnotherAppUser()
        {
            var user = context.Users.Find(-1);
            return user!.UserName;
        }

        [HttpGet("good")]
        public ActionResult<string> GetGood()
        {
            return "Request successful";
        }
    }
}