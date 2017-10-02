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

        public ICollection<IronManRuleModels> IronManRule { get; set; }

        public IEnumerable<ActivityLogModels> ActivityLog { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }


        public double TemmateTotalSwimDistance { get; set; }

        public IEnumerable<ParticpantStatsViewModel> ParticipantStats { get; set; }

        public string UsersTeam { get; set; }

        //properties for individual user stats test
        public double TotalSwimDistance { get; set; }

    }
}