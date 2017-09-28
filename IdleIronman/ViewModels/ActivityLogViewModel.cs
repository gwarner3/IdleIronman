using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdleIronman.Models;

namespace IdleIronman.ViewModels
{
    public class ActivityLogViewModel
    {
        public ActivityLogModels ActivityLog { get; set; }

        [Display(Name = "Exercise Completed")]
        public IEnumerable<ExerciseTypeModels> ExerciseType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}