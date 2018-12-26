using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IdleIronman.Models;

namespace IdleIronman.Controllers.API
{
    public class ActivitiesController : ApiController
    {
        private ApplicationDbContext _context;

        public ActivitiesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/activities
        public IEnumerable<ActivityLogModels> GetActivityLogs()
        {
           return  _context.ActivityLogs.ToList();
        }

        // POST an activity /api/activities/
        [HttpPost]
        public ActivityLogModels LogActivity(ActivityLogModels activity)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.ActivityLogs.Add(activity);
            _context.SaveChanges();

            return activity;
        }

        //PUT /api/activities/1
        [HttpPut]
        public void EditActivity(int id, ActivityLogModels activity)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var activityInDb = _context.ActivityLogs.SingleOrDefault(a => a.Id == id);

            if (activityInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            activityInDb.ActivityDate = activity.ActivityDate;
            activityInDb.Distance = activity.Distance;
            activityInDb.DurationInMinutes = activity.DurationInMinutes;
            activityInDb.ExerciseTypeModelsId = activity.ExerciseTypeModelsId;

            _context.SaveChanges();
        }

        // DELETE /api/activities/1
        [HttpDelete]
        public void DeleteActivity(int id)
        {
            var activityInDb = _context.ActivityLogs.SingleOrDefault(a => a.Id == id);

            if (activityInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.ActivityLogs.Remove(activityInDb);
            _context.SaveChanges();
        }


    }
}
