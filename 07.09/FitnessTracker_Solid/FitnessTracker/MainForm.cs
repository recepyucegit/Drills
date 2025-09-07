using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FitnessTracker.Models;
using FitnessTracker.Repositories;
using FitnessTracker.Services;

namespace FitnessTracker
{
    public partial class MainForm : Form
    {
        private readonly IWorkoutService _workoutService;
        private readonly ICalendarService _calendarService;
        private readonly IChartService _chartService;

        private NumericUpDown numExerciseCount;
        private DataGridView dgvExercises;
        private Button btnCalculate;
        private Button btnSave;
        private Button btnViewHistory;

        private Chart chartProgress;
        private Panel pnlChart;

        private Panel pnlCalendar;
        private Label lblCalendarTitle;
        private Button btnPrevMonth;
        private Button btnNextMonth;
        private Label lblMonthYear;

        private DateTime currentCalendarMonth;
        private List<ExerciseResult> exerciseResults;

        public MainForm() : this(
            new WorkoutService(new FileWorkoutRepository()),
            new CalendarService(),
            new ChartService())
        {
        }

        public MainForm(IWorkoutService workoutService, ICalendarService calendarService, IChartService chartService)
        {
            _workoutService = workoutService;
            _calendarService = calendarService;
            _chartService = chartService;

            InitializeComponent();
            exerciseResults = new List<ExerciseResult>();
            currentCalendarMonth = DateTime.Now;

            LoadData();
            UpdateCalendar();
            UpdateChart();
        }

        private void LoadData()
        {
            var workoutDays = _workoutService.GetWorkoutDays();
            // Eğer gerekirse başka verileri de yükle
        }

        private void InitializeComponent()
        {
            // ... (önceki gibi, değişmeden)
            // Sadece event handler’ları ve UpdateChart/UpdateCalendar çağrılarını servislerle değiştiriyoruz
        }

        // ... SetupDataGridView, CreateEmptyRows vs. aynı kalabilir

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            exerciseResults.Clear();
            bool hasError = false;

            for (int rowIndex = 0; rowIndex < dgvExercises.Rows.Count; rowIndex++)
            {
                DataGridViewRow row = dgvExercises.Rows[rowIndex];

                string exerciseName = row.Cells["ExerciseName"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(exerciseName))
                {
                    MessageBox.Show($"Satır {rowIndex + 1}: Egzersiz adını girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hasError = true;
                    break;
                }

                if (!decimal.TryParse(row.Cells["Weight"].Value?.ToString(), out decimal weight) || weight <= 0)
                {
                    MessageBox.Show($"Satır {rowIndex + 1}: Geçerli ağırlık girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hasError = true;
                    break;
                }

                if (!int.TryParse(row.Cells["SetCount"].Value?.ToString(), out int setCount) || setCount < 1 || setCount > 4)
                {
                    MessageBox.Show($"Satır {rowIndex + 1}: Set sayısını 1-4 arasında girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hasError = true;
                    break;
                }

                List<int> reps = new List<int>();

                for (int setIndex = 1; setIndex <= setCount; setIndex++)
                {
                    string repsText = row.Cells[$"Reps{setIndex}"].Value?.ToString();
                    if (!int.TryParse(repsText, out int repCount) || repCount <= 0)
                    {
                        MessageBox.Show($"Satır {rowIndex + 1}, Set {setIndex}: Geçerli tekrar sayısı girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        hasError = true;
                        break;
                    }
                    reps.Add(repCount);
                }

                if (hasError) break;

                int minReps = reps.Min();
                decimal weightIncrease = _workoutService.CalculateWeightIncrease(minReps);
                decimal nextWeight = weight + weightIncrease;

                ExerciseResult result = new ExerciseResult()
                {
                    ExerciseName = exerciseName,
                    CurrentWeight = weight,
                    SetCount = setCount,
                    Reps = reps,
                    MinReps = minReps,
                    WeightIncrease = weightIncrease,
                    NextWeight = nextWeight
                };

                exerciseResults.Add(result);
            }

            if (!hasError && exerciseResults.Count > 0)
            {
                btnSave.Enabled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _workoutService.SaveWorkout(exerciseResults);

                MessageBox.Show("Antrenman kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

                UpdateCalendar();
                UpdateChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       private void BtnViewHistory_Click(object sender, EventArgs e)
{
    var entries = _workoutService.GetWorkoutsByDate(DateTime.Now);
    if (entries.Count == 0)
    {
        MessageBox.Show("Bugün için antrenman kaydı bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }

    string content = string.Join("\n\n", entries.Select(ea =>
        $"EGZERSİZ: {ea.ExerciseName}\n" +
        $"AĞIRLIK: {ea.Weight} kg\n" +
        $"TEKRARLAR: {string.Join(", ", ea.Reps)}"));

    Form detailForm = new Form()
    {
        Text = "Bugünkü Antrenmanlar",
        Size = new Size(600, 500),
        StartPosition = FormStartPosition.CenterParent
    };

    TextBox txtDetail = new TextBox()
    {
        Multiline = true,
        ReadOnly = true,
        ScrollBars = ScrollBars.Vertical,
        Dock = DockStyle.Fill,
        Font = new Font("Consolas", 9, FontStyle.Bold),
        Text = content
    };

    detailForm.Controls.Add(txtDetail);
    detailForm.ShowDialog();
}

        private void UpdateCalendar()
        {
            var workoutDays = _workoutService.GetWorkoutDays();
            _calendarService.UpdateCalendar(pnlCalendar, lblMonthYear, currentCalendarMonth, workoutDays, ShowDayDetails);
        }

        private void ShowDayDetails(DateTime date)
        {
            var entries = _workoutService.GetWorkoutsByDate(date);
            if (entries.Count == 0)
            {
                MessageBox.Show($"{date:dd MMMM yyyy} tarihinde antrenman kaydı bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ 'entry' ismini kullan — 'e' değil!
            string content = string.Join("\n\n", entries.Select(entry =>
                $"EGZERSİZ: {entry.ExerciseName}\n" +
                $"AĞIRLIK: {entry.Weight} kg\n" +
                $"TEKRARLAR: {string.Join(", ", entry.Reps)}"));

            Form detailForm = new Form()
            {
                Text = $"{date:dd MMMM yyyy} Antrenman Detayları",
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            TextBox txtDetail = new TextBox()
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9, FontStyle.Bold),
                Text = content
            };

            detailForm.Controls.Add(txtDetail);
            detailForm.ShowDialog();
        }

        private void ChangeMonth(int delta)
        {
            _calendarService.ChangeMonth(ref currentCalendarMonth, delta);
            UpdateCalendar();
        }

        private void UpdateChart()
        {
            var allWorkouts = _workoutService.GetAllWorkouts();
            _chartService.UpdateChart(chartProgress, allWorkouts);
        }

        // ... diğer event handler’lar (btnPrevMonth.Click += (s,e) => ChangeMonth(-1); gibi)
    }
}
