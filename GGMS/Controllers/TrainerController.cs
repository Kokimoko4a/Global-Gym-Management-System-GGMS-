namespace GGMS.Web.Controllers
{
    using GGMS.Data.Models;
    using GGMS.Web.Infrastructure.Extensions;
    using GGMS.Web.ViewModels.Trainer;
    using GGMSServices.Data.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;
    

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;

        }

        [HttpGet]
        public IActionResult BecomeTrainer()
        {
            return View(new TainerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> BecomeTrainer(TainerViewModel trainerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();   
            }

            var userId = User.GetClaimValue(ClaimTypes.NameIdentifier);

           

            if (await trainerService.BecomeTrainer(trainerViewModel, Guid.Parse(userId))) 
            {
                return RedirectToAction("Index","Home");
            }

            return NoContent();
        }
      
    }
}
