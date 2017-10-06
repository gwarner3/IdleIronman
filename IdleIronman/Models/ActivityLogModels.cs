using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdleIronman.Models
{
    public class ActivityLogModels
    {
        public int Id { get; set; }

        [Display(Name = "Log Date")]
        [Required]
        public DateTime ActivityDate { get; set; }

        public double? Distance { get; set; }

        [Display(Name = "Duration")]
        public int? DurationInMinutes { get; set; }

        public ExerciseTypeModels ExerciseTypeModels { get; set; }

        [Display(Name = "Exercise Type")]
        public int ExerciseTypeModelsId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

    }
}