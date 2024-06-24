
namespace GGMS.Web.ViewModels.RequestToTrainer
{
    using GGMS.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.RequestToTrainer;
    public class RequestToTrainerFormModel
    { 
        public Guid IdOfTrainer { get; set; }
    
        public Guid IdOfClient { get; set; }

        public bool IsApproved { get; set; }

        public string DecriptionOfRequest { get; set; } = null!;
    }
}
