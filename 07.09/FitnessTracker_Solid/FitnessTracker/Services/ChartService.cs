using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using FitnessTracker.Models;

namespace FitnessTracker.Services
{
    public class ChartService : IChartService
    {
        public void UpdateChart(Chart chart, List<WorkoutEntry> allWorkouts)
        {
            chart.Series.Clear();

            var grouped = allWorkouts
                .GroupBy(e => e.ExerciseName)
                .ToDictionary(g => g.Key, g => g.OrderBy(e => e.Date).ToList());

            Color[] colors = {
                Color.Red, Color.Blue, Color.Green, Color.Purple,
                Color.Orange, Color.Brown, Color.Magenta, Color.Cyan,
                Color.DarkRed, Color.DarkBlue, Color.DarkGreen, Color.DarkGoldenrod
            };
            int colorIndex = 0;

            foreach (var kvp in grouped)
            {
                string exerciseName = kvp.Key;
                var points = kvp.Value;

                var series = new Series(exerciseName)
                {
                    ChartType = SeriesChartType.Line,
                    Color = colors[colorIndex % colors.Length],
                    BorderWidth = 3
                };

                foreach (var point in points)
                {
                    series.Points.AddXY(point.Date.ToString("dd MMM"), point.Weight);
                }

                chart.Series.Add(series);
                colorIndex++;
            }
        }
    }
}
