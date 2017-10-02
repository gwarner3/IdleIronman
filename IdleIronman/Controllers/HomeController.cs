using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdleIronman.Models;
using IdleIronman.ViewModels;
using Microsoft.AspNet.Identity;

namespace IdleIronman.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var swimId = 3;
            var bikeId = 2;
            var runId = 1;
            List<TeamModels> teams;
            ApplicationUser currentUser;
            var allStatsViewModel = new AllStatsViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();
                currentUser = _context.Users.Single(u => u.Id == currentUserId);
                teams = (from team in _context.Teams
                    where team.Teammates.Count > 0 &&
                          team.Id != currentUser.TeamModelsId
                    select team).Include(u=>u.TeamApplications).Include(r=>r.IronManRuleModels).ToList();
            }
            else
            {
                teams = _context.Teams.Where(c => c.Teammates.Count > 0).Select(t => t).ToList();
            }

            

            

            

            allStatsViewModel.Teams = teams;


            var eachUsersLog = _context.Users.Include(x => x.ActivityLog).ToList();

            var eachTeamsLog = eachUsersLog.GroupBy(t => t.TeamModelsId, (x, y) => y.Where(z => z.TeamModelsId == x).Aggregate<ApplicationUser, double?>(0.0, (r,t) => r += t.ActivityLog.Where(si=>si.ExerciseTypeModelsId == swimId).Sum(c => c.Distance) )).ToList();

            var totalTeamSwim = from i in _context.ActivityLogs
                where i.ExerciseTypeModelsId == swimId
                select new AllStatsViewModel()
                {
                    TemmateTotalSwimDistance = (double) i.Distance,
                };

            var totalSwimTest = _context.ActivityLogs.Where(t => t.ExerciseTypeModelsId == swimId)
                .GroupBy(s => s.Distance).ToList();

            //Code to view all participants data in table
                //get username, total swim, total bike, and total run
                    //place this in a list
            
            return View(allStatsViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}