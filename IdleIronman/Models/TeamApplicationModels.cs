using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdleIronman.Models
{
    public class TeamApplicationModels
    {
        public int Id { get; set; }

        [Display(Name = "Application Date")]
        [Required]
        public DateTime ApplicationDate { get; set; }

        [Display(Name = "Approval Status")]
        public bool IsApproved { get; set; }

        public bool WasDenied { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        public TeamModels TeamModels { get; set; }

        public int TeamModelsId { get; set; }
    }
}