using GGMS.Web.Infrastructure.Extensions;
using GGMSServices.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GGMS.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;

        public RequestController(IRequestService requestService)
        {
            this.requestService = requestService;
        }


        public IActionResult GetAllRequests()
        {
            if (User.Identity.IsAuthenticated)
            {
                Guid id = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

                return View(requestService.GetAllRequests(id));
            }

            return RedirectToAction("Login", "User");
        }
    }
}
