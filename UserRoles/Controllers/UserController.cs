using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRoles.Models;
using UserRoles.Models.DTOS;

namespace UserRoles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRoles.Models.AuthuserContext _context;

        public UserController(AuthuserContext context)
        {
            this._context = context;
        }

        [HttpPost("NewUser")]
        public async Task<IActionResult>NewUser([FromBody]AddNewUserDTO NewUserDto)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = NewUserDto.Name,
                Email = NewUserDto.Email,
                Password = NewUserDto.Password,
                RegTime = DateTime.Now
            };
            if(newUser is not null)
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return StatusCode(201, newUser.Id);
            }
            return StatusCode(400, "Failed to create user.");
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return StatusCode(200, users);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                return StatusCode(404, "User not found.");
            }
            return StatusCode(200, user);
        }
    }
}
