﻿using System;
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
            

            joiner.TeamModelsId = application.TeamModelsId;

            //Changing role
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            UserManager.AddToRole(joiner.Id, RoleNames.CanManagePersonalData);
            UserManager.RemoveFromRole(joiner.Id, RoleNames.CanManageTeamData);

            //Make changes to the application so it does not appear in list anymore
            application.IsApproved = true;

            _context.SaveChanges();

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                    Server.MapPath("~/Content/Images/"), pic);
                // file is uploaded
                file.SaveAs(path);

                var currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = _context.Users.Single(u => u.Id == currentUserId);
                var myTeam = _context.Teams.Single(x => x.Id == currentUser.TeamModelsId);
                myTeam.LinkToPhoto = pic;

                _context.SaveChanges();

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Deny(int id)
        {
            //change users team id and role to participant
            var application = _context.TeamApplications.Single(a => a.Id == id);

            //Make changes to the application so it does not appear in list anymore
            application.IsApproved = false;
            application.WasDenied = true;

            _context.SaveChanges();

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Delete(int id)
        {
            //change users team id and role to participant
            var application = _context.TeamApplications.Single(a => a.Id == id);

            _context.TeamApplications.Remove(application);

            _context.SaveChanges();

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult ChangePrivacy(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.Single(u => u.Id == currentUserId);

            var myTeam = _context.Teams.Single(x => x.Id == id);
            if (myTeam.IsPrivate)
            {
                myTeam.IsPrivate = false;
            }
            else
            {
                myTeam.IsPrivate = true;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Manage");
        }
    }
}