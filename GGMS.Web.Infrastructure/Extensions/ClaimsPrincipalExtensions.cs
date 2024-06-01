
namespace GGMS.Web.Infrastructure.Extensions
{


    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {

        public static string GetClaimValue(this ClaimsPrincipal user, string claimType)
        {
            var claim = user.FindFirst(claimType);
            return claim?.Value!;
        }
    }
}