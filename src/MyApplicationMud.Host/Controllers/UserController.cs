
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MyApplicationMud.Host.Controllers;

// orig src https://github.com/berhir/BlazorWebAssemblyCookieAuth
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetCurrentUser() => Ok(CreateUserInfo(User));

    private UserInfo CreateUserInfo(ClaimsPrincipal claimsPrincipal)
    {
        if (!claimsPrincipal?.Identity?.IsAuthenticated ?? true)
        {
            return UserInfo.Anonymous;
        }

        var userInfo = new UserInfo
        {
            IsAuthenticated = true
        };

        if (claimsPrincipal?.Identity is ClaimsIdentity claimsIdentity)
        {
            userInfo.NameClaimType = "name";
            userInfo.RoleClaimType = claimsIdentity.RoleClaimType;
        }
        else
        {
            userInfo.NameClaimType = "name";
            userInfo.RoleClaimType = ClaimTypes.Role;
        }

        if (claimsPrincipal?.Claims?.Any() ?? false)
        {
            var allClaims = claimsPrincipal.Claims
                .Select(u => new ClaimValue(u.Type, u.Value))
                .ToList();

            var claims = allClaims.DistinctBy(b => b.Type).ToList();

            userInfo.Claims = claims;
        }

        return userInfo;
    }
}
