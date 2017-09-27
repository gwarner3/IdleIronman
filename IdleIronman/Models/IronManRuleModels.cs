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
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "Duration")]
        [Required]
        public int DurationInDays { get; set; }

        [Display(Name = "Swim Distance")]
        [Required]
        public double SwimDistanceInMiles { get; set; }

        [Display(Name = "Bike Distance")]
        [Required]
        public double BikeDistancenIMiles { get; set; }

        [Display(Name = "Run Distance")]
        [Required]
        public double RunDistanceInMiles { get; set; }

        public IronManRuleModels()
        {
            SwimDistanceInMiles = 2.4;
            BikeDistancenIMiles = 112;
            RunDistanceInMiles = 26.2;
        }
    }
}