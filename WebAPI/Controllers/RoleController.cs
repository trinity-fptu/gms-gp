using Application.IServices;
using Application.ViewModels.Role;
using Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // test
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            return Ok(role);
        }

        [HttpGet]
        //[Authorize(Roles = UserRoleConst.INSPECTOR_STAFF)]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleAddVM model)
        {
            await _roleService.CreateAsync(model);
            return Ok("Role created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleVM model)
        {
            await _roleService.UpdateAsync(model);
            return Ok("Role updated successfully");
        }

    }
}
