using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using IdleIronman.Models;

namespace IdleIronman.Controllers.API
{
    public class ParticipantsController : ApiController
    {
        private ApplicationDbContext _context;

        public ParticipantsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/participants
        public IEnumerable<ExerciseTypeModels> GetExerciseTypeModels()
        {
            return _context.ExerciseTypes.ToList();
        }

        //GET a users activities /api/participants/stringID
        public IEnumerable<ActivityLogModels> GetActivityLog(string id)
        {
            return _context.ActivityLogs.Where(u => u.ApplicationUserId == id).ToList();
        }

        //get a single exercise type api/participants/id
        public ExerciseTypeModels GetExerciseType(int id)
        {
            var exerciseTpy = _context.ExerciseTypes.SingleOrDefault(e => e.Id == id);

            if (exerciseTpy == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return exerciseTpy;
        }

        // POST an activity /api/participants
        [HttpPost]
        public ActivityLogModels LoagActivity(ActivityLogModels activity)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.ActivityLogs.Add(activity);
            _context.SaveChanges();

            return activity;
        }

        //PUT /api/participants/1

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

        // DELETE /api/participant/1
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
