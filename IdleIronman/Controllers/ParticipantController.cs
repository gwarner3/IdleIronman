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
        private int waterAerobicsId;
        private int spinId;
        private int rowId;
        private double distanceComplete;
        private CalculateExerciseDistance _distanceCalculator;

        public ParticipantController()
        {
            _context = new ApplicationDbContext();
            currentUser = new ApplicationUser();
            distanceComplete = new double();
            swimId = 3;
            bikeId = 2;
            runId = 1;
            waterAerobicsId = 6;
            spinId = 5;
            rowId = 4;
            _distanceCalculator = new CalculateExerciseDistance();
        }
        
        // GET: Participant
        public ActionResult Index(string id)
        {
            var participantStatViewModel = new ParticpantStatsViewModel();
            string currentUserId;

            var currentViewerId = User.Identity.GetUserId();
            var currentViewer = _context.Users.Single(u => u.Id == currentViewerId);

            participantStatViewModel.CurrentViewer = currentViewer;

            if (id == null)
            {
                currentUserId = User.Identity.GetUserId();
            }
            else
            {
                currentUserId = id;
            }
            

            
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

            participantStatViewModel.ApplicationUser = currentUser;

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
                //////SOmething is happening to the date here
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
        public ActionResult DisplayStats()
        {

            var swimId = 3;
            var bikeId = 2;
            var runId = 1;
            List<TeamModels> teams;
            ApplicationUser currentUser;
            TeamStatsViewModel teamRecordsViewModel = new TeamStatsViewModel();
            TeamStatsListViewModel teamStatsListed = new TeamStatsListViewModel();
            teamStatsListed.TeamStats = new List<TeamStatsViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();
                currentUser = _context.Users.Single(u => u.Id == currentUserId);
                teams = (from team in _context.Teams
                        where team.Teammates.Count > 0 &&
                              //team.Id != currentUser.TeamModelsId &&
                              team.IsPrivate == false
                        select team).Include(u => u.TeamApplications)
                    .Include(r => r.IronManRuleModels)
                    .Include(m => m.Teammates)
                    .Include(td => td.TeamTotalSwimDistance).ToList();
            }
            else
            {
                teams = _context.Teams.Where(c => c.Teammates.Count > 0).Select(t => t)
                    .Include(r => r.IronManRuleModels)
                    .Include(m => m.Teammates)
                    .Include(td => td.TeamTotalSwimDistance).ToList();
            }

            

            for (int i = 0; i < teams.Count; i++)
            {
                var teamRecord = new TeamStatsViewModel();
                teamRecord.TeamId = teams[i].Id;

                teamRecord.TeamName = teams[i].Name;

                teamRecord.TeamPhotoName = teams[i].LinkToPhoto;

                teamRecord.StartDate = teams[i].IronManRuleModels.StartDate;

                var durationInDays = teams[i].IronManRuleModels.DurationInDays;
                var endDate = teams[i].IronManRuleModels.StartDate.AddDays(durationInDays);
                var daysLeft = (endDate - DateTime.Today).TotalDays;
                teamRecord.DaysLeft = daysLeft;

                teamRecord.Teammates = teams[i].Teammates.ToList();

                var eachUsersLog = _context.Users.Include(x => x.ActivityLog).ToList();

                var teamsLog = (from logs in eachUsersLog
                    where logs.TeamModelsId == teams[i].Id
                    select logs.ActivityLog.ToList()).ToList();

                for (int x = 0; x < teamsLog.Count(); x++)
                {
                    var thisLog = teamsLog[x];

                    //distanceComplete = 0;

                    foreach (ActivityLogModels t in thisLog)
                    {
                        if (t.ExerciseTypeModelsId == swimId || t.ExerciseTypeModelsId == rowId || t.ExerciseTypeModelsId == waterAerobicsId)
                        {
                            teamRecord.TotalSwimDistance += (double) t.Distance;
                            //distanceComplete += teamRecord.TotalSwimDistance;
                        }
                        else if (t.ExerciseTypeModelsId == bikeId || t.ExerciseTypeModelsId == spinId)
                        {
                            teamRecord.TotalBikeDistance += (double) t.Distance;
                            //distanceComplete += teamRecord.TotalBikeDistance;
                        }
                        else if (t.ExerciseTypeModelsId == runId)
                        {
                            teamRecord.TotalRunDistance += (double) t.Distance;
                            //distanceComplete += teamRecord.TotalRunDistance;
                        }
                    }
                }
                teamRecord.TotalDistanceComplete = teamRecord.TotalSwimDistance + teamRecord.TotalBikeDistance + teamRecord.TotalRunDistance;
                teamStatsListed.TeamStats.Add(teamRecord);
            }




            ////This passes correct photo
            return View(teamStatsListed);
        }

        public ActionResult MakeChart(double swam, double biked, double ran)
        {
            var chartModel = new ParticipantChartViewModel();

            var swimPercentComplete = (swam / 2.4) * 100;
            var runPercentComplete = (ran / 26.2) * 100;
            var bikePercentComplete = (biked / 112) * 100;

            chartModel.swam = swimPercentComplete;
            chartModel.biked = bikePercentComplete;
            chartModel.ran = runPercentComplete;
            return View(chartModel);
        }
    }
}
//var totalDistanceChart = new Chart(200, 200, ChartTheme.Blue)
//    .AddTitle("Total distance covered")
//    .AddLegend()
//    .AddSeries(
//    name: "Distance covered",
//    chartType: "bar",
//    xValue: new[] {"1 Distance", "2 Distance"},
//    yValues: new[] {"126", "63"});
//teamRecord.Chart = totalDistanceChart;

