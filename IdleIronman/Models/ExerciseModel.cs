using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdleIronman.Models
{
    public class ExerciseModel
    {
        public int Id { get; set; }

        [Display(Name = "Exercise")]
        public string Name { get; set; }
    }
}