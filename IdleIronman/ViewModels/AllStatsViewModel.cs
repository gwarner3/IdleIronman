using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdleIronman.Models;

namespace IdleIronman.ViewModels
{
    public class AllStatsViewModel
    {
        public IEnumerable<TeamModels> Teams { get; set; }

        public IEnumerable<IronManRuleModels> IronManRule { get; set; }

        public IEnumerable<ActivityLogModels> ActivityLog { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        public double TemmateTotalSwimDistance { get; set; }
        
    }
}