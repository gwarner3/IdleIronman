using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdleIronman.Models;

namespace IdleIronman.Helpers
{
    public class ActivityLogDateIsWithinRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var activityLog = (ActivityLogModels)validationContext.ObjectInstance;

            ApplicationDbContext _context = new ApplicationDbContext();
            //get the user ID
            //FInd their team
            //find their rules
            var userID = activityLog.ApplicationUserId;
            //var userTeam = 

            //var ironManRules = (IronManRuleModels) validationContext.ObjectInstance;
            //var endDateOfIIM =  ;

            //if (activityLog.ActivityDate == null)
            //{
                return ValidationResult.Success;
            //}
        }
    }
}