using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using FitnessTracker.Models;

namespace FitnessTracker.Repositories
{
    public interface IWorkoutRepository
    {
        void SaveWorkout(IEnumerable<ExerciseResult> results, DateTime workoutDate);
        Dictionary<DateTime, bool> LoadWorkoutDays();
        List<WorkoutEntry> LoadAllWorkouts();
        List<WorkoutEntry> LoadWorkoutsByDate(DateTime date);
    }
}
