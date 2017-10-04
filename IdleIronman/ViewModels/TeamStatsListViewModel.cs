using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdleIronman.ViewModels
{
    public class TeamStatsListViewModel
    {
        ////TEST of all items in list
        public List<TeamStatsViewModel> TeamStats { get; set; }

        public Array chartData { get; set; }
    }
}