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

namespace IdleIronman.Helpers
{
    public class CreateTeamStatsList
    {
        private static ApplicationDbContext _context;
        private static int swimId;
        private static int bikeId;
        private static int runId;
        private static int waterAerobicsId;
        private static int spinId;
        private static int rowId;

        static CreateTeamStatsList()
        {
            _context = new ApplicationDbContext();
            swimId = 3;
            bikeId = 2;
            runId = 1;
            waterAerobicsId = 6;
            spinId = 5;
            rowId = 4;


        }
        public static TeamStatsListViewModel GetTeamStatsList()
        {
            

            //var swimId = 3;
            //var bikeId = 2;
            //var runId = 1;
            List<TeamModels> teams;
            ApplicationUser currentUser;
            TeamStatsViewModel teamRecordsViewModel = new TeamStatsViewModel();
            TeamStatsListViewModel teamStatsListed = new TeamStatsListViewModel();
            teamStatsListed.TeamStats = new List<TeamStatsViewModel>();

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var currentUserId = HttpContext.Current.User.Identity.GetUserId();
                currentUser = _context.Users.Single(u => u.Id == currentUserId);
                teams = (from team in _context.Teams
                         where team.Teammates.Count > 0 &&
                               team.Id != currentUser.TeamModelsId &&
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
                            teamRecord.TotalSwimDistance += (double)t.Distance;
                            //distanceComplete += teamRecord.TotalSwimDistance;
                        }
                        else if (t.ExerciseTypeModelsId == bikeId || t.ExerciseTypeModelsId == spinId)
                        {
                            teamRecord.TotalBikeDistance += (double)t.Distance;
                            //distanceComplete += teamRecord.TotalBikeDistance;
                        }
                        else if (t.ExerciseTypeModelsId == runId)
                        {
                            teamRecord.TotalRunDistance += (double)t.Distance;
                            //distanceComplete += teamRecord.TotalRunDistance;
                        }
                    }
                }
                teamRecord.TotalDistanceComplete = teamRecord.TotalSwimDistance + teamRecord.TotalBikeDistance + teamRecord.TotalRunDistance;
                teamStatsListed.TeamStats.Add(teamRecord);
            }

            return teamStatsListed;
        }
    }
}