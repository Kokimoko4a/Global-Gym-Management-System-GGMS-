namespace GGMS.Web.Controllers
{
    using GGMS.Data.Models;
    using GGMS.Web.Infrastructure.Extensions;
    using GGMS.Web.ViewModels.FitnessProgram;
    using GGMS.Web.ViewModels.Trainer;
    using GGMSServices.Data.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using static GGMS.Common.NotificationMessagesConstants;

    [Authorize]
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;


        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;

        }

        [HttpGet]
        public async Task<IActionResult> BecomeTrainer()
        {
          

            if (await trainerService.IsTrainer(Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier))))
            {
                
              
                TempData[WarningMessage] = "You are already a trainer!";
                return RedirectToAction("Index", "Home");
            }

            return View(new TrainerFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> BecomeTrainer(TrainerFormModel trainerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }

            var userId = User.GetClaimValue(ClaimTypes.NameIdentifier);



            if (await trainerService.BecomeTrainer(trainerViewModel, Guid.Parse(userId)))
            {
                TempData[SuccessMessage] = "Now you are trainer ;)";

                return RedirectToAction("Index", "Home");
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProgram()
        {
            bool isTrainer = await trainerService.IsTrainer(Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier)));

            if (isTrainer)
            {
                return View(new FitnessProgramFormModel());
            }

            TempData[WarningMessage] = "You are not a trainer. Don't do shit!!!";
            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> CreateProgram(FitnessProgramFormModel fitnessProgramFormModel)
        {
            bool isTrainer = await trainerService.IsTrainer(Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier)));

            if (isTrainer)
            {
                fitnessProgramFormModel.IdOfTrainer = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));
                await trainerService.AddProgramAsync(fitnessProgramFormModel);

                TempData[SuccessMessage] = "Successfully added new fitness program.";
                return RedirectToAction("Index", "Home");
            }

            TempData[WarningMessage] = "You are not a trainer. Don't do shit!!!";
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> AllFitnessPrograms() 
        {
            return View(await trainerService.AllProgramsAsync(Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier))));


        }

        [HttpGet]
        public async Task<IActionResult> ViewProgram(Guid id)
        {
            return View(await trainerService.GetProgramAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProgram(Guid id)
        {
            await trainerService.DeleteProgramAsync(id);

            TempData[SuccessMessage] = "Successfully deleted program.";

            return RedirectToAction("AllFitnessPrograms", "Trainer");
        }

        [HttpGet]
        public async Task<IActionResult> EditProgram(Guid id)
        {
            FitnessProgramFormModel oldModel = await trainerService.GetProgramAsFormModel(id);

            return View(oldModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProgram(FitnessProgramFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View(formModel);
            }



            await trainerService.EditProgramAsync(formModel);

            TempData[SuccessMessage] = $"Successfully edited program called {formModel.Title}";

            return RedirectToAction("AllFitnessPrograms");
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo(Guid id)
        {
            return View(await trainerService.GetTrainer(Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier))));


        }
    }
}
