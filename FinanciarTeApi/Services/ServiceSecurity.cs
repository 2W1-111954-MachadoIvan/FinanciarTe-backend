using System.Security.Claims;

namespace FinanciarTeApi.Services
{
    public class ServiceSecurity : IServiceSecurity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceSecurity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? null;
        }

        public bool CheckUserHasroles(string[] roles)
        {
            var userRoles = (_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty).Split(",").ToList();

            return userRoles.Any() && userRoles.Any(x => roles.Contains(x));
        }
    }
}
