
namespace GGMS.Web.Infrastructure.Extensions
{
    using GGMSServices.Data.Interfaces;
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