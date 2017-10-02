using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdleIronman.Helpers;
using IdleIronman.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdleIronman.Controllers
{
    public class TeamCaptainController : Controller
    {
        private ApplicationDbContext _context;

        public TeamCaptainController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: TeamCaptain
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Accept(int id)
        {
            //change users team id and role to participant
            var application = _context.TeamApplications.Single(a=>a.Id == id);
            var joiner = _context.Users.Single(j => j.Id == application.ApplicationUserId);
            

            joiner.TeamModelsId = id;
            //Changing role
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            UserManager.AddToRole(joiner.Id, RoleNames.CanManagePersonalData);


            //Make changes to the application so it does not appear in list anymore
            application.IsApproved = true;

            _context.SaveChanges();

            return RedirectToAction("Index", "Manage");
        }
    }
}