using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdleIronman.ViewModels
{
    public class AdminChartViewModel
    {
        public double swam { get; set; }
        public double biked { get; set; }
        public double ran { get; set; }

        public string teamName { get; set; }
    }
}