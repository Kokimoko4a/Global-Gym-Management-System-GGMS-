using GGMS.Data.Models;
using GGMS.Web.ViewModels.RequestToTrainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMSServices.Data.Interfaces
{
    public interface IRequestService
    {
        public IEnumerable<RequestToTrainerViewModelSmall> GetAllRequestsForTrainer(Guid id);

        public IEnumerable<RequestToTrainerViewModelSmall> GetAllRequestsForUser(Guid id);

        public Task ApproveRequest(Guid id);

        public Task<bool> RequestExists(Guid id);
    }
}
