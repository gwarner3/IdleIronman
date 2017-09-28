using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdleIronman.Models;

namespace IdleIronman.ViewModels
{
    public class ParticpantStatsViewModel
    {
        public IEnumerable<ActivityLogModels> ActivityLogModels { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public IronManRuleModels IronManRules { get; set; }

        public double ParticipantTotalSwimDistance { get; set; }

        public double ParticipantTotalRunDistance { get; set; }

        public double ParticipantTotalBikedDistance { get; set; }

        public double DaysUntilEnd { get; set; }


    }
}