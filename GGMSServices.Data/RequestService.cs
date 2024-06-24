using GGMS.Data;
using GGMS.Data.Models;
using GGMS.Web.ViewModels.RequestToTrainer;
using GGMSServices.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMSServices.Data
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext data;

        public RequestService(ApplicationDbContext data)
        {
                this.data = data;
        }

        public IEnumerable<RequestToTrainerViewModelSmall> GetAllRequests(Guid id)
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
    }
}
