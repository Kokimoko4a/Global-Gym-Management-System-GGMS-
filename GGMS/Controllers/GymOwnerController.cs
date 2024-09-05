
namespace GGMS.Web.Controllers
{
    using GGMS.Web.Infrastructure.Extensions;
    using GGMS.Web.ViewModels.GymOwner;
    using GGMSServices.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using static GGMS.Common.NotificationMessagesConstants;

    public class GymOwnerController : Controller
    {
        private readonly IGymOwnerService gymOwnerService;

        public GymOwnerController(IGymOwnerService gymOwnerService)
        {
            this.gymOwnerService = gymOwnerService;
        }

        [HttpGet]
        public async Task<IActionResult> BecomeGymOwner()
        {
            Guid userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

            if (User.Identity.IsAuthenticated)
            {
                if (!await gymOwnerService.IsGymOwner(userId))
                {
                    await gymOwnerService.BecomeGymOwner(userId);
                    return RedirectToAction("Index", "Home");
                }

                TempData[WarningMessage] = "You are gym owner!";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public async Task<IActionResult> CreateGym()
        {
            if (User.Identity.IsAuthenticated)
            {
                Guid userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

                if (await gymOwnerService.IsGymOwner(userId))
                {
                    return View();
                }

                TempData[WarningMessage] = "You are not gym owner! You canonot create gym/s!";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

        }


        [HttpPost]
        public async Task<IActionResult> CreateGym(GymFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "Error when creating!";
                return View(formModel);
            }

            if (User.Identity.IsAuthenticated)
            {
                Guid userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

                await gymOwnerService.CreateGym(formModel,userId);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

        }

    }

}

