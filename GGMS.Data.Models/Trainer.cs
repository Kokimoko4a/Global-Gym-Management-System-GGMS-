﻿namespace GGMS.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.TrainerValidationConstants;

    public class Trainer
    {
        public Trainer()
        {
            FitnessPrograms = new HashSet<FitnessProgram>();
            Clients = new HashSet<ApplicationUser>();
            Requests = new HashSet<RequestToTrainer>();
            Comments = new HashSet<CommentTrainer>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        public ICollection<FitnessProgram> FitnessPrograms { get; set; } = null!;

        public ICollection<ApplicationUser> Clients { get; set; } = null!;

        [Required]
        [StringLength(BioghraphyMaxLength , MinimumLength = BioghraphyMinLength)]
        public string Biography { get; set; } = null!;

        public ICollection<RequestToTrainer> Requests { get; set; }

        public ICollection<CommentTrainer> Comments { get; set; } = null!;

        //this property is neccesary for assigning fitness programs!!!
        public Guid IdOfClientCurrentlyWorkingWith { get; set; }
    }
}
