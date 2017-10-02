using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using IdleIronman.Helpers;
using IdleIronman.Models;
using IdleIronman.ViewModels;
using Microsoft.AspNet.Identity;

namespace IdleIronman.Controllers
{
    public class ParticipantController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUser currentUser;
        private int swimId;
        private int bikeId;
        private int runId;
        private CalculateExerciseDistance _distanceCalculator;

        public ParticipantController()
        {
            _context = new ApplicationDbContext();
            currentUser = new ApplicationUser();
            swimId = 3;
            bikeId = 2;
            runId = 1;
            _distanceCalculator = new CalculateExerciseDistance();
        }
        
        // GET: Participant
        public ActionResult Index()
        {
            var participantStatViewModel = new ParticpantStatsViewModel();
            var currentUserId = User.Identity.GetUserId();

            
            //grabbing the current user
            try
            {
                currentUser = _context.Users.Single(u => u.Id == currentUserId);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
                throw;
            }

            //getting current users team name
            var usersTeam = (from team in _context.Teams
                where  team.Id == currentUser.TeamModelsId
                select team.Name).ToString();


            
            //must figure out way to remove magic int 3
            var totalSwimDistance = (from participant in _context.ActivityLogs
                where participant.ExerciseTypeModelsId == swimId &&
                participant.ApplicationUserId == currentUserId
                select participant.Distance).Sum();

            if (totalSwimDistance != null)
            {
                participantStatViewModel.ParticipantTotalSwimDistance = (double)totalSwimDistance;
            }

            var totalBikeDistance = (from participant in _context.ActivityLogs
                where participant.ExerciseTypeModelsId == bikeId &&
                      participant.ApplicationUserId == currentUserId
                select participant.Distance).Sum();

            if (totalBikeDistance != null)
            {
                participantStatViewModel.ParticipantTotalBikedDistance = (double)totalBikeDistance;
            }

            var totalRunDistance = (from participant in _context.ActivityLogs
                where participant.ExerciseTypeModelsId == runId &&
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

            var usersLog = (from log in _context.ActivityLogs
                where log.ApplicationUserId == currentUserId
                select log).Include(x => x.ExerciseTypeModels);

            participantStatViewModel.ActivityLogModels = usersLog;

            var exerciseTypes = from item in _context.ExerciseTypes
                select item;

            participantStatViewModel.ExerciseType = exerciseTypes;


            return View(participantStatViewModel);
        }

        //Post: /JoinTeam
        public ActionResult JoinTeam(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _context.Users.Single(u => u.Id == currentUserId);
            var teamApplication = new TeamApplicationModels();
            
            teamApplication.ApplicationDate = DateTime.Now;
            teamApplication.ApplicationUserId = currentUserId;
            teamApplication.TeamModelsId = id;

            _context.TeamApplications.Add(teamApplication);
            _context.SaveChanges();


            return RedirectToAction("Index", "Home");  
        }

        //GET: LogActivity
        public ActionResult LogActivity()
        {
            var exerciseTypes = _context.ExerciseTypes.ToList();

            var activityViewModel = new ActivityLogViewModel
            {
                ExerciseType = exerciseTypes,
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

            string appUserId = User.Identity.GetUserId();
            double? distance;

            if (activityViewModel.ActivityLog.ExerciseTypeModelsId != bikeId &&
                activityViewModel.ActivityLog.ExerciseTypeModelsId != swimId &&
            activityViewModel.ActivityLog.ExerciseTypeModelsId != runId)
            {
                distance = _distanceCalculator.GetDistance(activityViewModel);
            }
            else
            {
                distance = activityViewModel.ActivityLog.Distance;
            }

            var activityLog = new ActivityLogModels
            {
                ActivityDate = activityViewModel.ActivityLog.ActivityDate,
                Distance = distance,
                DurationInMinutes = activityViewModel.ActivityLog.DurationInMinutes,
                ExerciseTypeModelsId = activityViewModel.ActivityLog.ExerciseTypeModelsId,
                ApplicationUserId = appUserId
            };

            var actvityLogIdTest = activityViewModel.ActivityLog.Id;
            var actvityLogIdTest2 = activityLog.Id;


            if (activityViewModel.ActivityLog.Id == 0)
            {
                _context.ActivityLogs.Add(activityLog);
            }
            else
            {
                var activityInLog = _context.ActivityLogs.Single(currentActivity => currentActivity.Id == activityViewModel.ActivityLog.Id);
                activityInLog.ActivityDate = activityLog.ActivityDate;
                activityInLog.Distance = activityLog.Distance;
                activityInLog.DurationInMinutes = activityLog.DurationInMinutes;
                activityInLog.ExerciseTypeModelsId = activityLog.ExerciseTypeModelsId;
            }
            
            _context.SaveChanges();

            return RedirectToAction("LogActivity", "Participant");
        }

        public ActionResult Edit(int id)
        {
            var activityLog = _context.ActivityLogs.SingleOrDefault(u => u.Id == id);
            if (activityLog == null)
            {
                return HttpNotFound();
            }

            string appUserId = User.Identity.GetUserId();

            var activityLogViewModel = new ActivityLogViewModel
            {
                ApplicationUser = _context.Users.Single(u => u.Id == appUserId),
                ExerciseType = _context.ExerciseTypes.ToList(),
                ActivityLog = activityLog
            };

            return View("LogActivity", activityLogViewModel);
        }

        public ActionResult Delete(int id)
        {
            ActivityLogModels logToDelete = _context.ActivityLogs.First(deleteLog => deleteLog.Id == id);
            _context.ActivityLogs.Remove(logToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index", "Participant");
        }

        public ActionResult AllParticipantStats()
        {
            return View();
        }

        //public ActionResult ShowChart()
        //{
        //        var progressChart = new Chart(width: 400, height: 400)
        //            .AddTitle("Exercise Progress")
        //            .AddSeries(
        //                name: "Progress",
        //                xValue: new[] { "Swim", "Bike", "Run" },
        //                yValues: new[] { ParticipantTotalSwimDistance, ParticipantTotalBikedDistance, ParticipantTotalRunDistance })
        //            .Write();

        //    return File(progressChart, "image/bytes");
        //}
    }
}