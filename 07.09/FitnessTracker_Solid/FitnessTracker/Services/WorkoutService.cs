using System;
using System.Collections.Generic;
using System.Linq;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _repository;

        public WorkoutService(IWorkoutRepository repository)
        {
            _repository = repository;
        }

        public decimal CalculateWeightIncrease(int minReps)
        {
            if (minReps <= 5)
                return 0m;
            else if (minReps == 6)
                return 1.25m;
            else if (minReps == 7)
                return 2.5m;
            else if (minReps >= 8)
                return 5m;
            else
                return 0m;
        }

        public void SaveWorkout(IEnumerable<ExerciseResult> results)
        {
            _repository.SaveWorkout(results, DateTime.Now);
        }

        public Dictionary<DateTime, bool> GetWorkoutDays()
        {
            return _repository.LoadWorkoutDays();
        }

        public List<WorkoutEntry> GetAllWorkouts()
        {
            return _repository.LoadAllWorkouts();
        }

        public List<WorkoutEntry> GetWorkoutsByDate(DateTime date)
        {
            return _repository.LoadWorkoutsByDate(date);
        }
    }
}