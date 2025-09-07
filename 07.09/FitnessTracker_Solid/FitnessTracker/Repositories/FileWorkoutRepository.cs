using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FitnessTracker.Models;

namespace FitnessTracker.Repositories
{
    public class FileWorkoutRepository : IWorkoutRepository
    {
        private readonly string _logsFolder;

        public FileWorkoutRepository()
        {
            _logsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker", "DailyLogs");
            Directory.CreateDirectory(_logsFolder);
        }

        public void SaveWorkout(IEnumerable<ExerciseResult> results, DateTime workoutDate)
        {
            string filePath = GetDailyFilePath(workoutDate.Date);

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine($"================================");
                sw.WriteLine($"İDMAN TARİHİ: {workoutDate:dd.MM.yyyy HH:mm}");
                sw.WriteLine($"TOPLAM EGZERSİZ: {results.Count()}");
                sw.WriteLine($"================================\n");

                foreach (var result in results)
                {
                    sw.WriteLine($"EGZERSİZ: {result.ExerciseName}");
                    sw.WriteLine($"AĞIRLIK: {result.CurrentWeight} kg");
                    sw.WriteLine($"SET SAYISI: {result.SetCount}");

                    for (int i = 0; i < result.Reps.Count; i++)
                    {
                        sw.WriteLine($"  {i + 1}. Set: {result.Reps[i]} tekrar");
                    }

                    sw.WriteLine($"En Düşük Tekrar: {result.MinReps}");

                    if (result.WeightIncrease > 0)
                    {
                        sw.WriteLine($"Sonraki Ağırlık: {result.NextWeight} kg (+{result.WeightIncrease} kg)");
                    }
                    else
                    {
                        sw.WriteLine($"Sonraki Ağırlık: {result.NextWeight} kg (Artırım Yok - Tekrar Sayısını Arttır)");
                    }

                    sw.WriteLine(new string('-', 40) + "\n");
                }
            }
        }

        public Dictionary<DateTime, bool> LoadWorkoutDays()
        {
            var days = new Dictionary<DateTime, bool>();

            var dailyFiles = Directory.GetFiles(_logsFolder, "*.txt")
                .Where(f => Path.GetFileName(f).Length == 14 && Path.GetFileName(f)[2] == '.' && Path.GetFileName(f)[5] == '.')
                .ToList();

            foreach (string file in dailyFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                if (DateTime.TryParseExact(fileName, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime workoutDate))
                {
                    days[workoutDate.Date] = true;
                }
            }

            return days;
        }

        public List<WorkoutEntry> LoadAllWorkouts()
        {
            var entries = new List<WorkoutEntry>();

            var dailyFiles = Directory.GetFiles(_logsFolder, "*.txt")
                .Where(f => Path.GetFileName(f).Length == 14 && Path.GetFileName(f)[2] == '.' && Path.GetFileName(f)[5] == '.')
                .OrderBy(f => File.GetCreationTime(f))
                .ToList();

            foreach (string file in dailyFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                if (!DateTime.TryParseExact(fileName, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fileDate))
                    continue;

                var lines = File.ReadAllLines(file);
                string currentExercise = "";
                decimal currentWeight = 0;
                List<int> currentReps = new List<int>();

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    if (line.StartsWith("EGZERSİZ: "))
                    {
                        currentExercise = line.Replace("EGZERSİZ: ", "").Trim();
                    }
                    else if (line.StartsWith("AĞIRLIK: "))
                    {
                        string weightPart = line.Replace("AĞIRLIK: ", "").Split(' ')[0];
                        if (decimal.TryParse(weightPart, out decimal w))
                        {
                            currentWeight = w;
                        }
                    }
                    else if (line.Contains(". Set: "))
                    {
                        string repPart = line.Split(':')[1].Trim();
                        if (int.TryParse(repPart, out int rep))
                        {
                            currentReps.Add(rep);
                        }
                    }
                    else if (line.StartsWith(new string('-', 40)))
                    {
                        if (!string.IsNullOrEmpty(currentExercise) && currentReps.Count > 0)
                        {
                            entries.Add(new WorkoutEntry(fileDate, currentExercise, currentWeight, new List<int>(currentReps)));
                            currentReps.Clear();
                        }
                    }
                }
            }

            return entries;
        }

        public List<WorkoutEntry> LoadWorkoutsByDate(DateTime date)
        {
            string filePath = GetDailyFilePath(date.Date);
            if (!File.Exists(filePath)) return new List<WorkoutEntry>();

            var entries = new List<WorkoutEntry>();
            var lines = File.ReadAllLines(filePath);
            string currentExercise = "";
            decimal currentWeight = 0;
            List<int> currentReps = new List<int>();

            foreach (string line in lines)
            {
                if (line.StartsWith("EGZERSİZ: "))
                {
                    currentExercise = line.Replace("EGZERSİZ: ", "").Trim();
                }
                else if (line.StartsWith("AĞIRLIK: "))
                {
                    string weightPart = line.Replace("AĞIRLIK: ", "").Split(' ')[0];
                    if (decimal.TryParse(weightPart, out decimal w))
                    {
                        currentWeight = w;
                    }
                }
                else if (line.Contains(". Set: "))
                {
                    string repPart = line.Split(':')[1].Trim();
                    if (int.TryParse(repPart, out int rep))
                    {
                        currentReps.Add(rep);
                    }
                }
                else if (line.StartsWith(new string('-', 40)))
                {
                    if (!string.IsNullOrEmpty(currentExercise) && currentReps.Count > 0)
                    {
                        entries.Add(new WorkoutEntry(date.Date, currentExercise, currentWeight, new List<int>(currentReps)));
                        currentReps.Clear();
                    }
                }
            }

            return entries;
        }

        private string GetDailyFilePath(DateTime date)
        {
            string fileName = date.ToString("dd.MM.yyyy") + ".txt";
            return Path.Combine(_logsFolder, fileName);
        }
    }
}
