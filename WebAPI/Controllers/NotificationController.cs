using Application.IServices;
using Application.ViewModels.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _notificationService.GetByUserId(userId);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of read notification of a user
        /// </summary>
        [HttpGet("user/{userId}/read")]
        public async Task<IActionResult> GetReadNotificationByUserId(int userId)
        {
            var result = await _notificationService.GetReadNotificationByUserId(userId);
            return Ok(result);
        }

        /// <summary>
        /// Get the list of unread notification of a user
        /// </summary>
        [HttpGet("user/{userId}/unread")]
        public async Task<IActionResult> GetUnreadNotificationByUserId(int userId)
        {
            var result = await _notificationService.GetUnreadNotificationByUserId(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _notificationService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NotificationAddVM model)
        {
            await _notificationService.AddAsync(model);
            return Ok("Notification create successfully");
        }

        /// <summary>
        /// Set the notification as read
        /// </summary>
        [HttpGet("read/{id}")]
        public async Task<IActionResult> ReadNotification(int id)
        {
            await _notificationService.ReadNotificationAsync(id);
            return Ok("Notification read successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationService.GetAllAsync();
            return Ok(result);
        }
    }
}
