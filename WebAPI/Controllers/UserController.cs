using Application;
using Application.IServices;
using Application.ResponseModels;
using Application.Utils;
using Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWarehouseService _warehouseService;
        public UserController(IUserService userService, IUnitOfWork unitOfWork, IWarehouseService warehouseService)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _warehouseService = warehouseService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(UserAddVM model)
        {
            await _userService.CreateAsync(model);
            return Ok("User created successfully");
        }

        [Authorize]
        [HttpPost("supplier")]
        public async Task<IActionResult> Create(UserSupplierAddVM model)
        {
            await _userService.CreateAsync(model);
            return Ok("Supplier created successfully");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetByRoleId(int roleId)
        {
            var user = await _userService.GetAllByRoleAsync(roleId);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }


        /// <summary>
        /// Get the user info by supplier id
        /// </summary>
        [Authorize]
        [HttpGet("/supplier/{supplierId}")]
        public async Task<IActionResult> GetBySupplierId(int supplierId)
        {
            var user = await _userService.GetBySupplierId(supplierId);
            return Ok(user);
        }

        /// <summary>
        /// Get the user info by manager id
        /// </summary>
        [Authorize]
        [HttpGet("/manager/{managerId}")]
        public async Task<IActionResult> GetByManagerIdId(int managerId)
        {
            var user = await _userService.GetByManagerId(managerId);
            return Ok(user);
        }

        /// <summary>
        /// Get the user info by purchasingManager id
        /// </summary>
        [Authorize]
        [HttpGet("/purchasingManager/{purchasingManagerId}")]
        public async Task<IActionResult> GetByPurchasingManagerId(int purchasingManagerId)
        {
            var user = await _userService.GetByPurchasingManagerId(purchasingManagerId);
            return Ok(user);
        }

        /// <summary>
        /// Get the user info by purchasingStaff id
        /// </summary>
        [Authorize]
        [HttpGet("/purchasingStaff/{purchasingStaffId}")]
        public async Task<IActionResult> GetByPurchasingStaffId(int purchasingStaffId)
        {
            var user = await _userService.GetByPurchasingStaffId(purchasingStaffId);
            return Ok(user);
        }

        /// <summary>
        /// Get the user info by warehouseStaff id
        /// </summary>
        [Authorize]
        [HttpGet("/warehouseStaff/{warehouseStaffId}")]
        public async Task<IActionResult> GetByWarehouseStaffId(int warehouseStaffId)
        {
            var user = await _userService.GetByWarehouseStaffId(warehouseStaffId);
            return Ok(user);
        }

        /// <summary>
        /// Get the user info by inspector id
        /// </summary>
        [Authorize]
        [HttpGet("/inspector/{inspectorId}")]
        public async Task<IActionResult> GetByInspectorId(int inspectorId)
        {
            var user = await _userService.GetByInspectorId(inspectorId);
            return Ok(user);
        }

        /// <summary>
        /// Get the user info by staff code
        /// </summary>
        [Authorize]
        [HttpGet("/staffCode/{code}")]
        public async Task<IActionResult> GetByStaffCode(string code)
        {
            var user = await _userService.GetByStaffCodeAsync(code);
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UserUpdateVM model)
        {
            await _userService.UpdateAsync(model);
            return Ok("User updated successfully");
        }

        [HttpPut("supplier")]
        [Authorize]
        public async Task<IActionResult> Create(UserSupplierUpdateVM model)
        {
            await _userService.UpdateAsync(model);
            return Ok("Supplier updated successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginVM userLoginVM)
        {
            var token = await _userService.LoginAsync(userLoginVM);
            return Ok(new { Token = token });
        }
        [HttpPost("change-all-password")]
        public async Task<IActionResult> ChangeAllPassword(string password)
        {
            var userList = await _unitOfWork.UserRepo.GetAllAsync();

            foreach (var user in userList)
            {
                user.HashedPassword = password.Hash();
            }

            _unitOfWork.UserRepo.UpdateRange(userList);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Change All Password success");
        }

        [HttpGet("time")]
        public async Task<IActionResult> GetTime()
        {

            await _warehouseService.UpdateMissingWarehouseMaterial();
            return Ok(new
            {
                NowTime = DateTime.Now,
                Today = DateTime.Today,
                Timezone = TimeZone.CurrentTimeZone,
                TimezoneStandardName = TimeZone.CurrentTimeZone.StandardName
            });
        }


    }
}
