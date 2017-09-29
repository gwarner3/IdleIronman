using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdleIronman.Helpers;
using IdleIronman.Models;

namespace IdleIronman.ViewModels
{
    public class ActivityLogViewModel
    {
        public ActivityLogModels ActivityLog { get; set; }

        //may use variable below to validate date entry
        //public DateTime activityDate { get; set; }

        [Display(Name = "Exercise Completed")]
        public IEnumerable<ExerciseTypeModels> ExerciseType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}