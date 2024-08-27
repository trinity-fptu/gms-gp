namespace Application.Services
{
    public interface IClaimsService
    {
        public int GetCurrentUserId { get; }
        public int GetCurrentRoleId { get; }
    }
}
