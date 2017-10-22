using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IdleIronman.Helpers;
using IdleIronman.Models;
using IdleIronman.ViewModels;
using Microsoft.Owin.Security.Google;

namespace IdleIronman.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var activityLogs = db.ActivityLogs.Include(a => a.ApplicationUser).Include(a => a.ExerciseTypeModels);


            return View(activityLogs.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityLogModels activityLogModels = db.ActivityLogs.Find(id);
            if (activityLogModels == null)
            {
                return HttpNotFound();
            }
            return View(activityLogModels);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            ViewBag.ExerciseTypeModelsId = new SelectList(db.ExerciseTypes, "Id", "Name");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ActivityDate,Distance,DurationInMinutes,ExerciseTypeModelsId,ApplicationUserId")] ActivityLogModels activityLogModels)
        {
            if (ModelState.IsValid)
            {
                db.ActivityLogs.Add(activityLogModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", activityLogModels.ApplicationUserId);
            ViewBag.ExerciseTypeModelsId = new SelectList(db.ExerciseTypes, "Id", "Name", activityLogModels.ExerciseTypeModelsId);
            return View(activityLogModels);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityLogModels activityLogModels = db.ActivityLogs.Find(id);
            if (activityLogModels == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", activityLogModels.ApplicationUserId);
            string userId = activityLogModels.ApplicationUserId;
            activityLogModels.ApplicationUser = db.Users.Single(u => u.Id == userId);
            ViewBag.ExerciseTypeModelsId = new SelectList(db.ExerciseTypes, "Id", "Name", activityLogModels.ExerciseTypeModelsId);
            return View(activityLogModels);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ActivityDate,Distance,DurationInMinutes,ExerciseTypeModelsId,ApplicationUserId")] ActivityLogModels activityLogModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityLogModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string userId = activityLogModels.ApplicationUserId;
            activityLogModels.ApplicationUser = db.Users.Single(u => u.Id == userId);

            ViewBag.ExerciseTypeModelsId = new SelectList(db.ExerciseTypes, "Id", "Name", activityLogModels.ExerciseTypeModelsId);
            return View(activityLogModels);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityLogModels activityLogModels = db.ActivityLogs.Find(id);
            if (activityLogModels == null)
            {
                return HttpNotFound();
            }
            return View(activityLogModels);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityLogModels activityLogModels = db.ActivityLogs.Find(id);
            db.ActivityLogs.Remove(activityLogModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult DisplayCharts()
        {
            TeamStatsListViewModel teamstatsList = CreateTeamStatsList.GetTeamStatsList();

            return View(teamstatsList);
        }

        public ActionResult MakeChart(double swam, double biked, double ran, string teamName)
        {

            AdminChartViewModel adminChart = new AdminChartViewModel();

            adminChart.swam = swam;
            adminChart.biked = biked;
            adminChart.ran = ran;
            adminChart.teamName = teamName;

            return View(adminChart);
        }
    }
}
