using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRoles.Models;
using UserRoles.Models.DTOS;

namespace UserRoles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserRoles.Models.AuthuserContext _context;

        public RolesController(AuthuserContext context)
        {
            _context = context;
        }

        [HttpPost("NewRole")]
        public async Task<IActionResult> NewRole([FromBody]AddNewRoleDTO addNewRole)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = addNewRole.Name,
                RegTime = DateTime.Now
            };

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return StatusCode(201, role.Id);
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return StatusCode(200, roles);
        }

        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is null)
            {
                return StatusCode(404, "Role not found.");
            }
            return StatusCode(200, role);
        }

        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] UpdateRoleDTO updateRoleDto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is null)
            {
                return StatusCode(404, "Role not found.");
            }
            role.Name = updateRoleDto.Name;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Role updated successfully.");
        }

        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is null)
            {
                return StatusCode(404, "Role not found.");
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Role deleted successfully.");
        }
    }
}
