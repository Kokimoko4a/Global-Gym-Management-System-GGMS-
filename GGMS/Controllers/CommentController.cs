namespace GGMS.Web.Controllers
{
    using GGMS.Web.Infrastructure.Extensions;
    using GGMSServices.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using static GGMS.Common.NotificationMessagesConstants;

    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentTrainerFormModel model)
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

        [HttpPost]
        public async Task<IActionResult> LikeComment( [FromBody] LikeCommentTrainer model) 
        {
          

            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(User.GetClaimValue(ClaimTypes.NameIdentifier));
                Guid commentId = Guid.Parse(model.CommentId);

                int likes = await commentService.LikeComment(userId,commentId);

                Console.WriteLine(likes);

                return Json(likes);
            }

            

            return BadRequest();
        }



        public class LikeCommentTrainer
        {
            public string CommentId { get; set; }
        }

        public class AddCommentTrainerFormModel
        {
            public string TrainerId { get; set; }
            public string Content { get; set; }
        }
    }
}
