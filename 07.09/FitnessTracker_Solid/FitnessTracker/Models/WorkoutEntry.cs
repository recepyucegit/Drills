using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class WorkoutEntry
    {
        public DateTime Date { get; set; }
        public string ExerciseName { get; set; }
        public decimal Weight { get; set; }
        public List<int> Reps { get; set; }

        public WorkoutEntry(DateTime date, string exerciseName, decimal weight, List<int> reps)
        {
            Date = date;
            ExerciseName = exerciseName;
            Weight = weight;
            Reps = reps;
        }
    }
}
