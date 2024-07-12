namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.RequestToTrainer;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext data;

        public RequestService(ApplicationDbContext data)
        {
            this.data = data;
        }


        public IEnumerable<RequestToTrainerViewModelSmall> GetAllRequestsForTrainer(Guid id)
        {
            return data.RequestToTrainers.Where(x => x.IdOfTrainer == id)
                .Select(x => new RequestToTrainerViewModelSmall()
                {
                    DecriptionOfRequest = x.DecriptionOfRequest,
                    Id = x.Id,
                    IdOfClient = x.IdOfClient,
                    IdOfTrainer = x.IdOfTrainer,
                    IsApproved = x.IsApproved
                });
        }


        public async Task ApproveRequest(Guid id)
        {
            RequestToTrainer request = await data.RequestToTrainers.FirstOrDefaultAsync(r => r.Id == id)!;

            request.IsApproved = true;

            Trainer trainer = await data.Trainers.FirstOrDefaultAsync(x => x.Id == request.IdOfTrainer);

            ApplicationUser user = await data.Users.FirstOrDefaultAsync(x => x.Id == request.IdOfClient);

            trainer.Clients.Add(user);

            await data.SaveChangesAsync();
        }

        public async Task<bool> RequestExists(Guid id)
        {
            RequestToTrainer request = await data.RequestToTrainers.FirstOrDefaultAsync(r => r.Id == id)!;

            return request != null ? true : false;
        }

        public IEnumerable<RequestToTrainerViewModelSmall> GetAllRequestsForUser(Guid id)
        {
            return data.RequestToTrainers.Where(r => r.IdOfClient == id)
                .Select(r => new RequestToTrainerViewModelSmall()
                {
                    DecriptionOfRequest = r.DecriptionOfRequest,
                    Id = r.Id,
                    IdOfClient = r.IdOfClient,
                    IdOfTrainer = r.IdOfTrainer,
                    IsApproved= r.IsApproved
                });
        }
    }
}
