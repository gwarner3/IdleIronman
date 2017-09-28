using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdleIronman.Helpers;

namespace IdleIronman.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {

            if (User.IsInRole(RoleNames.CanManagePersonalData))
            {
                return RedirectToAction("Index", "Participant");
            }

            if (User.IsInRole(RoleNames.CanManageTeamData))
            {
                return RedirectToAction("Index", "TeamCaptain");
            }

            if (User.IsInRole(RoleNames.CanManageAllData))
            {
                return RedirectToAction("Index", "TeamCaptain");
            }


            return RedirectToAction("Index", "Home");
        }
    }
}