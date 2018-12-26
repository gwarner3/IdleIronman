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
        //public IEnumerable<ExerciseTypeModels> GetExerciseTypeModels()
        //{
        //    return _context.ExerciseTypes.ToList();
        //}

        // GET /api/participants
        public IEnumerable<ApplicationUser> GetParticipants()
        {
            return _context.Users.ToList();
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
    }
}
