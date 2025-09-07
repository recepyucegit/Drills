using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace FitnessTracker
{
    public partial class MainForm : Form
    {
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

        private Dictionary<DateTime, bool> workoutDays;
        private DateTime currentCalendarMonth;

        private List<ExerciseResult> exerciseResults;

        public MainForm()
        {
            InitializeComponent();
            exerciseResults = new List<ExerciseResult>();
            workoutDays = new Dictionary<DateTime, bool>();
            currentCalendarMonth = DateTime.Now;
            LoadWorkoutDaysFromFile();
            UpdateCalendar();
            UpdateChart();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(900, 700);
            this.Text = "Fitness Takip Uygulaması - Çoklu Egzersiz";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Exercise Count
            Label lblExerciseCount = new Label()
            {
                Text = "Egzersiz Sayısı:",
                Location = new Point(20, 20),
                Size = new Size(120, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            this.Controls.Add(lblExerciseCount);

            numExerciseCount = new NumericUpDown()
            {
                Location = new Point(150, 18),
                Size = new Size(100, 25),
                Font = new Font("Arial", 10, FontStyle.Bold),
                Minimum = 1,
                Maximum = 10,
                Value = 3
            };
            numExerciseCount.ValueChanged += NumExerciseCount_ValueChanged;
            this.Controls.Add(numExerciseCount);

            // DataGridView for exercises
            dgvExercises = new DataGridView()
            {
                Location = new Point(20, 60),
                Size = new Size(850, 200),
                Font = new Font("Arial", 9, FontStyle.Bold),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvExercises.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(dgvExercises);

            SetupDataGridView();

            // Calculate button
            btnCalculate = new Button()
            {
                Text = "Hesapla",
                Location = new Point(20, 280),
                Size = new Size(100, 40),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.LightBlue,
                UseVisualStyleBackColor = false
            };
            btnCalculate.Click += BtnCalculate_Click;
            this.Controls.Add(btnCalculate);

            // Save button
            btnSave = new Button()
            {
                Text = "Kaydet",
                Location = new Point(130, 280),
                Size = new Size(100, 40),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.LightGreen,
                UseVisualStyleBackColor = false,
                Enabled = false
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            // View History button
            btnViewHistory = new Button()
            {
                Text = "Geçmiş Görüntüle",
                Location = new Point(240, 280),
                Size = new Size(120, 40),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.LightYellow,
                UseVisualStyleBackColor = false
            };
            btnViewHistory.Click += BtnViewHistory_Click;
            this.Controls.Add(btnViewHistory);

            // === GRAFİK PANELI (SOL) ===
            Label lblChartTitle = new Label()
            {
                Text = "AĞIRLIK GELİŞİM GRAFİĞİ",
                Location = new Point(20, 340),
                Size = new Size(450, 25),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblChartTitle);

            pnlChart = new Panel()
            {
                Location = new Point(20, 370),
                Size = new Size(450, 280),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            this.Controls.Add(pnlChart);

            chartProgress = new Chart()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var chartArea = new ChartArea("MainArea")
            {
                AxisX = { Title = "Tarih", LabelStyle = { Angle = -45, Font = new Font("Arial", 9, FontStyle.Bold) }, TitleFont = new Font("Arial", 10, FontStyle.Bold) },
                AxisY = { Title = "Ağırlık (kg)", LabelStyle = { Font = new Font("Arial", 9, FontStyle.Bold) }, TitleFont = new Font("Arial", 10, FontStyle.Bold) }
            };
            chartProgress.ChartAreas.Add(chartArea);

            chartProgress.Legends.Add(new Legend("Legend")
            {
                Docking = Docking.Bottom,
                Font = new Font("Arial", 10, FontStyle.Bold)
            });

            pnlChart.Controls.Add(chartProgress);

            // === TAKVİM PANELI (SAĞ) ===
            lblCalendarTitle = new Label()
            {
                Text = "ANTRENMAN TAKVİMİ",
                Location = new Point(480, 340),
                Size = new Size(400, 25),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblCalendarTitle);

            btnPrevMonth = new Button()
            {
                Text = "←",
                Location = new Point(480, 370),
                Size = new Size(40, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            btnPrevMonth.Click += (s, e) => ChangeMonth(-1);
            this.Controls.Add(btnPrevMonth);

            lblMonthYear = new Label()
            {
                Text = "",
                Location = new Point(530, 370),
                Size = new Size(150, 30),
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblMonthYear);

            btnNextMonth = new Button()
            {
                Text = "→",
                Location = new Point(690, 370),
                Size = new Size(40, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            btnNextMonth.Click += (s, e) => ChangeMonth(1);
            this.Controls.Add(btnNextMonth);

            pnlCalendar = new Panel()
            {
                Location = new Point(480, 410),
                Size = new Size(400, 240),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            this.Controls.Add(pnlCalendar);
        }

        private void SetupDataGridView()
        {
            dgvExercises.Columns.Clear();

            dgvExercises.Columns.Add("ExerciseName", "Egzersiz Adı");
            dgvExercises.Columns["ExerciseName"].Width = 150;

            dgvExercises.Columns.Add("Weight", "Ağırlık (kg)");
            dgvExercises.Columns["Weight"].Width = 100;

            dgvExercises.Columns.Add("SetCount", "Set Sayısı");
            dgvExercises.Columns["SetCount"].Width = 80;

            dgvExercises.Columns.Add("Reps1", "1.Set");
            dgvExercises.Columns.Add("Reps2", "2.Set");
            dgvExercises.Columns.Add("Reps3", "3.Set");
            dgvExercises.Columns.Add("Reps4", "4.Set");

            for (int i = 1; i <= 4; i++)
            {
                dgvExercises.Columns[$"Reps{i}"].Width = 60;
            }

            CreateEmptyRows();
        }

        private void NumExerciseCount_ValueChanged(object sender, EventArgs e)
        {
            CreateEmptyRows();
        }

        private void CreateEmptyRows()
        {
            dgvExercises.Rows.Clear();
            int exerciseCount = (int)numExerciseCount.Value;

            for (int i = 0; i < exerciseCount; i++)
            {
                int rowIndex = dgvExercises.Rows.Add();
                DataGridViewRow row = dgvExercises.Rows[rowIndex];
                row.Cells["SetCount"].Value = "3";
            }
        }

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
                decimal weightIncrease = CalculateWeightIncrease(minReps);
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
                DateTime now = DateTime.Now;
                string dailyFilePath = GetDailyFilePath(now.Date);

                using (StreamWriter sw = File.AppendText(dailyFilePath))
                {
                    sw.WriteLine($"================================");
                    sw.WriteLine($"İDMAN TARİHİ: {now.ToString("dd.MM.yyyy HH:mm")}");
                    sw.WriteLine($"TOPLAM EGZERSİZ: {exerciseResults.Count}");
                    sw.WriteLine($"================================\n");

                    foreach (var result in exerciseResults)
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

                MessageBox.Show($"Antrenman kaydedildi!\nDosya: {dailyFilePath}", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSave.Enabled = false;

                DateTime today = DateTime.Now.Date;
                workoutDays[today] = true;
                UpdateCalendar();
                UpdateChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewHistory_Click(object sender, EventArgs e)
        {
            try
            {
                string logsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker", "DailyLogs");
                string filePath = Path.Combine(logsFolder, DateTime.Now.ToString("dd.MM.yyyy") + ".txt");

                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);

                    Form historyForm = new Form()
                    {
                        Text = "Bugünkü Antrenman Geçmişi",
                        Size = new Size(700, 600),
                        StartPosition = FormStartPosition.CenterParent
                    };

                    TextBox txtHistory = new TextBox()
                    {
                        Multiline = true,
                        ReadOnly = true,
                        ScrollBars = ScrollBars.Vertical,
                        Dock = DockStyle.Fill,
                        Font = new Font("Consolas", 9, FontStyle.Bold),
                        Text = content
                    };

                    historyForm.Controls.Add(txtHistory);
                    historyForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Henüz kayıtlı antrenman bulunamadı!", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dosya okuma hatası: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDailyFilePath(DateTime date)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker", "DailyLogs");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = date.ToString("dd.MM.yyyy") + ".txt";
            return Path.Combine(folderPath, fileName);
        }

        private void UpdateCalendar()
        {
            pnlCalendar.Controls.Clear();
            lblMonthYear.Text = currentCalendarMonth.ToString("MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));

            string[] dayHeaders = { "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt", "Paz" };
            for (int i = 0; i < 7; i++)
            {
                Label dayLabel = new Label()
                {
                    Text = dayHeaders[i],
                    Location = new Point(i * 50 + 10, 5),
                    Size = new Size(40, 20),
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.LightGray
                };
                pnlCalendar.Controls.Add(dayLabel);
            }

            DateTime firstDayOfMonth = new DateTime(currentCalendarMonth.Year, currentCalendarMonth.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            int startDayIndex = (int)firstDayOfMonth.DayOfWeek;
            if (startDayIndex == 0) startDayIndex = 7;
            startDayIndex--;

            int day = 1;
            int y = 30;

            for (int week = 0; week < 6; week++)
            {
                for (int col = 0; col < 7; col++)
                {
                    int x = col * 50 + 10;

                    if (week == 0 && col < startDayIndex) continue;
                    if (day > lastDayOfMonth.Day) break;

                    DateTime currentDate = new DateTime(currentCalendarMonth.Year, currentCalendarMonth.Month, day);
                    Label dayBox = new Label()
                    {
                        Text = day.ToString(),
                        Location = new Point(x, y),
                        Size = new Size(40, 40),
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = currentDate.Date == DateTime.Today ? Color.LightYellow : Color.White,
                        Tag = currentDate
                    };

                    if (currentDate.Date == DateTime.Today)
                    {
                        dayBox.Font = new Font("Arial", 10, FontStyle.Bold);
                    }

                    if (workoutDays.ContainsKey(currentDate.Date) && workoutDays[currentDate.Date])
                    {
                        dayBox.Text += "\n✗";
                        dayBox.ForeColor = Color.Green;
                        dayBox.Font = new Font("Arial", 10, FontStyle.Bold);
                    }

                    dayBox.Click += DayBox_Click;
                    pnlCalendar.Controls.Add(dayBox);
                    day++;
                    if (day > lastDayOfMonth.Day) break;
                }
                y += 40;
                if (day > lastDayOfMonth.Day) break;
            }
        }

        private void ChangeMonth(int delta)
        {
            currentCalendarMonth = currentCalendarMonth.AddMonths(delta);
            UpdateCalendar();
        }

        private void DayBox_Click(object sender, EventArgs e)
        {
            if (sender is Label dayBox && dayBox.Tag is DateTime clickedDate)
            {
                string logsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker", "DailyLogs");
                string filePath = Path.Combine(logsFolder, clickedDate.ToString("dd.MM.yyyy") + ".txt");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"{clickedDate:dd MMMM yyyy} tarihinde antrenman kaydı bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string content = File.ReadAllText(filePath);

                Form detailForm = new Form()
                {
                    Text = $"{clickedDate:dd MMMM yyyy} Antrenman Detayları",
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
        }

        private void LoadWorkoutDaysFromFile()
        {
            workoutDays.Clear();
            string logsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker", "DailyLogs");

            if (!Directory.Exists(logsFolder)) return;

            var dailyFiles = Directory.GetFiles(logsFolder, "*.txt")
                .Where(f => Path.GetFileName(f).Length == 14 && Path.GetFileName(f)[2] == '.' && Path.GetFileName(f)[5] == '.')
                .ToList();

            foreach (string file in dailyFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                if (DateTime.TryParseExact(fileName, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime workoutDate))
                {
                    workoutDays[workoutDate.Date] = true;
                }
            }
        }

        private void UpdateChart()
        {
            chartProgress.Series.Clear();

            string logsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker", "DailyLogs");
            if (!Directory.Exists(logsFolder)) return;

            var dailyFiles = Directory.GetFiles(logsFolder, "*.txt")
                .Where(f => Path.GetFileName(f).Length == 14 && Path.GetFileName(f)[2] == '.' && Path.GetFileName(f)[5] == '.')
                .OrderBy(f => File.GetCreationTime(f))
                .ToList();

            var exerciseData = new Dictionary<string, List<(DateTime Date, decimal Weight)>>();

            foreach (string file in dailyFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                if (!DateTime.TryParseExact(fileName, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fileDate))
                    continue;

                var lines = File.ReadAllLines(file);
                string currentExercise = "";
                decimal currentWeight = 0;

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

                            if (!exerciseData.ContainsKey(currentExercise))
                                exerciseData[currentExercise] = new List<(DateTime, decimal)>();

                            exerciseData[currentExercise].Add((fileDate, currentWeight));
                        }
                    }
                }
            }

            Color[] colors = {
                Color.Red, Color.Blue, Color.Green, Color.Purple,
                Color.Orange, Color.Brown, Color.Magenta, Color.Cyan,
                Color.DarkRed, Color.DarkBlue, Color.DarkGreen, Color.DarkGoldenrod
            };
            int colorIndex = 0;

            foreach (var kvp in exerciseData)
            {
                string exerciseName = kvp.Key;
                var points = kvp.Value.OrderBy(p => p.Date).ToList();

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

                chartProgress.Series.Add(series);
                colorIndex++;
            }
        }

        private decimal CalculateWeightIncrease(int minReps)
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
    }

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