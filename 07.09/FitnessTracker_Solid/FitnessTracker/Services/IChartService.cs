using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using FitnessTracker.Models;

namespace FitnessTracker.Services
{
    public interface IChartService
    {
        void UpdateChart(Chart chart, List<WorkoutEntry> allWorkouts);
    }
}
