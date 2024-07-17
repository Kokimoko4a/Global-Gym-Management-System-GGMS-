namespace Recipe_Sharing_Platform.Web.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Microsoft.Extensions.Caching.Memory;

    using System.Text;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.User;
    using GGMS.Web.Infrastructure.Extensions;
    using System.Security.Claims;
    using GGMSServices.Data.Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using static GGMS.Common.GeneralApplicationConstants;


    // using System.Web.SessionState.HttpSessionState;

    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;
        private readonly IRequestService requestService;
        private readonly IUserService userService;


        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IMemoryCache memoryCache,
                              IRequestService requestService,
                              IUserService userService
                             )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.memoryCache = memoryCache;
            this.requestService = requestService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                TelephoneNumber = model.TelephoneNumber,
                Age = model.Age,
                Address = model.Address
            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);

            // memoryCache.Remove(UsersCacheKey);

            //   TempData[SuccessMessage] = "Succesfully made account";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
             await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

             LoginFormModel model = new LoginFormModel()
             {
                 ReturnUrl = returnUrl
             };

             return View(model);

     
        }

          [HttpPost]
          public async Task<IActionResult> Login(LoginFormModel model)
          {
              if (!ModelState.IsValid)
              {
                  return View(model);
              }

              var result =
                  await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

              if (!result.Succeeded)
              {
                 // TempData[ErrorMessage] =
                    //  "There was an error while logging you in! Your password can be incorrect! Please try again or contact an administrator.";



                  return View(model);
              }

              return Redirect(model.ReturnUrl ?? "/Home/Index");
          }

         [HttpGet]
         public async Task<IActionResult> ViewProfile()
         {
            if (User.Identity.IsAuthenticated)
            {
                Guid id = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

                try
                {
                    var user = await userService.GetApplicationUserAsync(id);

                    return View(user);
                }
                catch (Exception)
                {

                    return BadRequest();
                }

             
            }

            return BadRequest();
         }

        [HttpPost]
        public async Task<IActionResult> UploadImage(Guid id, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {

                var user = await userService.GetApplicationUserAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                // Define the directory path and the file name
                var uploadsDirectory = Path.Combine(WwwRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

                // Create the directory if it doesn't exist
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                // Save the file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                await userService.SaveImagePathAsync(uniqueFileName, id);
            }

            return RedirectToAction("ViewProfile", new { id });
        }

        /* [HttpPost]
         public async Task<IActionResult> DeleteAccount(Guid id)
         {
             if (await userService.GetAllInfoAboutUserByIdAsync(id) == null)
             {
                 return BadRequest();
             }

             if (User.GetId() != id.ToString())
             {
                 return BadRequest();
             }

             await userService.DeleteUserInfo(id);


             return RedirectToAction("Logout", "User");

         }*/



        public async Task<IActionResult> Logout()
         {
             await signInManager.SignOutAsync();

             //TempData[SuccessMessage] = "You successfully deleted your account";

             return RedirectToAction("Index", "Home");
         }

        public async Task<IActionResult> GetResponses()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));

                return View(requestService.GetAllRequestsForUser(userId));
            }

            return RedirectToAction("Login", "User");


        }



         /*public async Task<FileResult> DownloadUserData(Guid id)
         {
             /* Retrieve user data and serialize it*/
        /*var userData = await userService.GetUserDataForCurrentUser(id);
        var serializedData = JsonConvert.SerializeObject(userData); /*Convert the serialized data to bytes*/
        /*   byte[] dataBytes = Encoding.UTF8.GetBytes(serializedData);
           /*Set the content type and return the file for download*/
        // return File(dataBytes, "application/json", "user_data.json");
    }
}


