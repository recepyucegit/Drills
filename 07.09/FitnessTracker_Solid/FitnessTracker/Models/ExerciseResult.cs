using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class ExerciseResult
    {
        public string ExerciseName { get; set; }
        public decimal CurrentWeight { get; set; }
        public int SetCount { get; set; }
        public List<int> Reps { get; set; }
        public int MinReps { get; set; }
        public decimal WeightIncrease { get; set; }
        public decimal NextWeight { get; set; }
    }
}
