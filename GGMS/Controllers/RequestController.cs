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

                return View(requestService.GetAllRequestsForTrainer(id));
            }

            return RedirectToAction("Login", "User");
        }

        public async Task<IActionResult> ApproveRequest(Guid id)
        {     
            if (User.Identity.IsAuthenticated)
            {
             

                if (await requestService.RequestExists(id))
                {
                    await requestService.ApproveRequest(id);

                    return RedirectToAction("GetAllRequests", "Request");
                }

                return BadRequest();
            }

            return RedirectToAction("Login", "User");
        }
    }
}
