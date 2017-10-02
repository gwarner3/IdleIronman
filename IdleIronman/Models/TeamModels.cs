using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdleIronman.Models
{
    public class TeamModels
    {
        public int Id { get; set; }

        [Display(Name = "Team Name")]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public string LinkToPhoto { get; set; }

        public IronManRuleModels IronManRuleModels { get; set; }

        public int IronManRuleModelsId { get; set; }

        public ICollection<ApplicationUser> Teammates { get; set; }

        public ICollection<TeamApplicationModels> TeamApplications { get; set; }

    }
}