using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdleIronman.Models;
using IdleIronman.ViewModels;
using Microsoft.AspNet.Identity;

namespace IdleIronman.Controllers
{
    public class ParticipantController : Controller
    {
        private ApplicationDbContext _context;

        public ParticipantController()
        {
            _context = new ApplicationDbContext();
        }
        
        // GET: Participant
        public ActionResult Index()
        {
            var participantStatViewModel = new ParticpantStatsViewModel();
            var currentUserId = User.Identity.GetUserId();
            //grabbing the current user
            ApplicationUser currentUser = _context.Users.Single(u => u.Id == currentUserId);

            //must figure out way to remove magic int 3
            var totalSwimDistance = (from participant in _context.ActivityLogs
                where participant.ExerciseTypeModelsId == 3 &&
                participant.ApplicationUserId == currentUserId
                select participant.Distance).Sum();

            if (totalSwimDistance != null)
            {
                participantStatViewModel.ParticipantTotalSwimDistance = (double)totalSwimDistance;
            }

            var totalBikeDistance = (from participant in _context.ActivityLogs
                where participant.ExerciseTypeModelsId == 2 &&
                      participant.ApplicationUserId == currentUserId
                select participant.Distance).Sum();

            if (totalBikeDistance != null)
            {
                participantStatViewModel.ParticipantTotalBikedDistance = (double)totalBikeDistance;
            }

            var totalRunDistance = (from participant in _context.ActivityLogs
                where participant.ExerciseTypeModelsId == 1 &&
                      participant.ApplicationUserId == currentUserId
                select participant.Distance).Sum();

            if (totalRunDistance != null)
            {
                participantStatViewModel.ParticipantTotalRunDistance = (double)totalRunDistance;
            }

            //get rule Id
            var startDate = (from team in _context.Teams
                where team.Id == currentUser.TeamModelsId
                select team.IronManRuleModels.StartDate).Single();

            //get rule duration
            var durationInDays = (from team in _context.Teams
                where team.Id == currentUser.TeamModelsId
                select team.IronManRuleModels.DurationInDays).Single();

            var endDate = startDate.AddDays(durationInDays);

            var daysUntilEnd = (endDate - DateTime.Today).TotalDays;

            if (daysUntilEnd > 0 && daysUntilEnd < durationInDays)
            {
                participantStatViewModel.DaysUntilEnd = daysUntilEnd;
            }
            else
            {
                participantStatViewModel.DaysUntilEnd = 0;
            }

            var usersLog = from log in _context.ActivityLogs
                where log.ApplicationUserId == currentUserId
                select log;

            participantStatViewModel.ActivityLogModels = usersLog;


            return View(participantStatViewModel);
        }

        //GET: LogActivity
        public ActionResult LogActivity()
        {
            var exerciseTypes = _context.ExerciseTypes.ToList();

            var activityViewModel = new ActivityLogViewModel
            {
                ExerciseType = exerciseTypes
            };

            return View(activityViewModel);
        }

        //POST: LogActivity
        [HttpPost]
        public ActionResult LogActivity(ActivityLogViewModel activityViewModel)
        {
            //ToDoItems
            //Ensure ActivityDate is not before iimstart date and not after startdate + duration
            //Some sort of validation needed to ensure values are not null

            var appUserId = User.Identity.GetUserId();

            var activityLog = new ActivityLogModels
            {
                ActivityDate = activityViewModel.ActivityLog.ActivityDate,
                Distance = activityViewModel.ActivityLog.Distance,
                DurationInMinutes = activityViewModel.ActivityLog.DurationInMinutes,
                ExerciseTypeModelsId = activityViewModel.ActivityLog.ExerciseTypeModelsId,
                ApplicationUserId = appUserId
            };

            _context.ActivityLogs.Add(activityLog);
            _context.SaveChanges();

            var exerciseTypes = _context.ExerciseTypes.ToList();
            activityViewModel.ExerciseType = exerciseTypes;

            return RedirectToAction("LogActivity", "Participant");
        }
    }
}