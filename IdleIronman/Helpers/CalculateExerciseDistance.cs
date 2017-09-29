using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdleIronman.ViewModels;

namespace IdleIronman.Helpers
{
    public class CalculateExerciseDistance
    {
        private double? distance;
        private const int RowId = 4;
        private const double RowMultiplier = .0266;
        private const int SpinId = 5;
        private const double SpinMultiplier = .5;
        private const int WaterAerobicsId = 6;
        private const double WaterAerobicsMultiplier = .01333;


        public CalculateExerciseDistance()
        {
            distance = new double();
        }

        public double? GetDistance(ActivityLogViewModel activityLogViewModel)
        {
            switch (activityLogViewModel.ActivityLog.ExerciseTypeModelsId)
            {
                case RowId:
                    distance = RowMultiplier * activityLogViewModel.ActivityLog.DurationInMinutes;
                    break;
                case SpinId:
                    distance = SpinMultiplier * activityLogViewModel.ActivityLog.DurationInMinutes;
                    break;
                case WaterAerobicsId:
                    distance = WaterAerobicsMultiplier * activityLogViewModel.ActivityLog.DurationInMinutes;
                    break;
            }
            return distance;
        }
    }
}