namespace WebAPI.Services
{
    using Application.Services;
    using System.Security.Claims;

    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                var userId = httpContextAccessor.HttpContext.User.FindFirstValue("UserId");
                GetCurrentUserId = userId == null ? -1 : int.Parse(userId);
                var roleId = httpContextAccessor.HttpContext.User.FindFirstValue("RoleId");
                GetCurrentRoleId = roleId == null ? -1 : int.Parse(roleId);
            }
        }
        public int GetCurrentUserId { get; }
        public int GetCurrentRoleId { get; }
    }
}
