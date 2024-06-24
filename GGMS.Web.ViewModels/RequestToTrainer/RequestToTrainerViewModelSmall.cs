using GGMS.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GGMS.Web.ViewModels.RequestToTrainer
{
    public class RequestToTrainerViewModelSmall
    {
        public Guid Id { get; set; }

        public Guid IdOfTrainer { get; set; }
     
        public Guid IdOfClient { get; set; }

        public bool IsApproved { get; set; }

        public string DecriptionOfRequest { get; set; } = null!;
    }
}
