using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdleIronman.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {

            if (User.IsInRole("CanManagePersonalData"))
            {
                return RedirectToAction("Index", "Participant");
            }

            if (User.IsInRole("CanManageTeamData"))
            {
                return RedirectToAction("Index", "TeamCaptain");
            }


            return RedirectToAction("Index", "Home");
        }
    }
}