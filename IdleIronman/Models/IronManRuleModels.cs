using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdleIronman.Models
{
    public class IronManRuleModels
    {
        public int Id { get; set; }

        [Display(Name = "Start")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Swim Distance")]
        public double SwimDistanceInMiles { get; set; }

        [Display(Name = "Bike Distance")]
        public double BikeDistancenIMiles { get; set; }

        [Display(Name = "Run Distance")]
        public double RunDistanceInMiles { get; set; }
    }
}