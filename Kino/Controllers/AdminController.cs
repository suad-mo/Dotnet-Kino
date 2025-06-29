using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kino.Core.ViewModels;
using Kino.Services.Roles;
using Kino.Services.User;

namespace Kino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        private readonly IUserService _userService;

        public AdminController(IRolesService rolesService, IUserService userService)
        {
            _rolesService = rolesService;
            _userService = userService;
        }

        [HttpPost("Role")]
        public async Task<ActionResult> AddRole(RoleViewModel roleViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Došlo je do greške.");

                var result = await _rolesService.AddRole(roleViewModel);

                if (result == null)
                    return BadRequest("Došlo je do greške prilikom kreiranja uloge.");

                return Ok(result);
            }
            catch
            {

                return BadRequest("Došlo je do greške.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("User")]
        public async Task<ActionResult> AddUser(UpdateUserViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Došlo je do greške.");

                var result = await _userService.AddUser(user);

                if (result == null)
                    return BadRequest("Došlo je do greške prilikom kreiranja korisnika.");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom spašavanja korisnika.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("User")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                return Ok(users);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom dohvaćanja korisnika.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("User")]
        public async Task<ActionResult> UpdateUser(UpdateUserViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Došlo je do greške.");

                var result = await _userService.UpdateUser(user);

                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom ažuriranja korisnika.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("User")]
        public async Task<ActionResult> DeleteUser(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return BadRequest("Email ne može biti prazan.");

                var result = await _userService.DeleteUser(email);

                //if (!result.Succeeded)
                //    return BadRequest("Došlo je do greške prilikom brisanja korisnika.");
                
                return Ok(result);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom brisanja korisnika.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("User/{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return BadRequest("ID ne može biti prazan.");

                var user = await _userService.GetUser(id);

                if (user == null)
                    return NotFound("Korisnik nije pronađen.");

                return Ok(user);
            }
            catch
            {
                return BadRequest("Došlo je do greške prilikom dohvaćanja korisnika.");
            }
        }
    }
}
