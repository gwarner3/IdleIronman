using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using IdleIronman.Models;

namespace IdleIronman.ViewModels
{
    public class TeamStatsViewModel
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string TeamPhotoName { get; set; }

        public DateTime StartDate { get; set; }

        public double DaysLeft { get; set; }

        public double TotalRunDistance { get; set; }
        public double TotalBikeDistance { get; set; }
        public double TotalSwimDistance { get; set; }

        public Chart Chart { get; set; }

        public List<ApplicationUser> Teammates { get; set; }

        public double TotalDistanceComplete { get; set; }
    }
}