using GGMS.Web.Infrastructure.Extensions;
using GGMSServices.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GGMS.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid userId;
            if (!Guid.TryParse(User.GetClaimValue(ClaimTypes.NameIdentifier), out userId))
            {
                return Unauthorized();
            }

            try
            {
                Guid trainerId = Guid.Parse(model.TrainerId);

                await commentService.CreteComment(userId, trainerId, model.Content);

                // Fetch updated comments
                var comments = commentService.GetCommentsForTrainer(trainerId);

                return Json(comments); // Return updated comments as JSON
            }
            catch (FormatException ex)
            {
                Console.WriteLine(111);
                // Handle the case where trainerId could not be parsed as Guid
                return BadRequest("Invalid trainerId format");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(121212);
                // Log the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while adding the comment");
              
            }
        }

        public class AddCommentModel
        {
            public string TrainerId { get; set; }
            public string Content { get; set; }
        }
    }
}
