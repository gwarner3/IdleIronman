using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdleIronman.Models;

namespace IdleIronman.ViewModels
{
    public class AdminViewModel
    {
        public ActivityLogModels ActivityLog { get; set; }

        public IEnumerable<ExerciseTypeModels> ExerciseTypes { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        
    }
}