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

            //must figure out way to remove magic int 3
            var totalSwimDistance = (from participant in _context.ActivityLogs
                where participant.ExerciseTypeModelsId == 3 &&
                participant.ApplicationUserId == currentUserId
                select participant.Distance).Sum();

            if (totalSwimDistance != null)
            {
                participantStatViewModel.ParticipantTotalSwimDistance = (double)totalSwimDistance;
            }
                                    

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