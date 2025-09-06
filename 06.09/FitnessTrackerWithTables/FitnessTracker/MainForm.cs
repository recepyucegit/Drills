using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTracker
{
    public partial class MainForm : Form
    {
        private NumericUpDown numExerciseCount;
        private DataGridView dgvExercises;
        private Button btnCalculate;
        private Button btnSave;
        private Button btnViewHistory;
        private Panel pnlWorkoutTable;
        private string currentWorkoutData = "";
        private List<ExerciseResult> exerciseResults;

        public MainForm()
        {
            InitializeComponent();
            exerciseResults = new List<ExerciseResult>();
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
                Font = new Font("Arial", 10),
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
                Font = new Font("Arial", 9),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
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

            // Workout Summary Label
            Label lblSummaryTitle = new Label()
            {
                Text = "İDMAN TABLOSU:",
                Location = new Point(20, 340),
                Size = new Size(200, 25),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblSummaryTitle);

            // Panel for workout table
            pnlWorkoutTable = new Panel()
            {
                Location = new Point(20, 370),
                Size = new Size(850, 280),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                BackColor = Color.White
            };
            this.Controls.Add(pnlWorkoutTable);
        }

        private void SetupDataGridView()
        {
            dgvExercises.Columns.Clear();

            // Exercise Name
            dgvExercises.Columns.Add("ExerciseName", "Egzersiz Adı");
            dgvExercises.Columns["ExerciseName"].Width = 150;

            // Weight
            dgvExercises.Columns.Add("Weight", "Ağırlık (kg)");
            dgvExercises.Columns["Weight"].Width = 100;

            // Set Count - Normal TextBox column olarak
            dgvExercises.Columns.Add("SetCount", "Set Sayısı");
            dgvExercises.Columns["SetCount"].Width = 80;

            // Reps columns
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

                row.Cells["SetCount"].Value = "3"; // Default 3 sets
            }
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            exerciseResults.Clear();
            bool hasError = false;

            for (int rowIndex = 0; rowIndex < dgvExercises.Rows.Count; rowIndex++)
            {
                DataGridViewRow row = dgvExercises.Rows[rowIndex];

                // Validation
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

                // Calculate results for this exercise
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
                DisplayWorkoutTable();
                PrepareWorkoutData();
                btnSave.Enabled = true;
            }
        }

        private void DisplayWorkoutTable()
        {
            pnlWorkoutTable.Controls.Clear();

            // Create table header
            Label headerLabel = new Label()
            {
                Text = "EGZERSİZ                    MEVCUT KİLO    SETLER        EN DÜŞÜK   SONRAKİ KİLO   ARTIŞ",
                Location = new Point(10, 10),
                Size = new Size(820, 25),
                Font = new Font("Consolas", 10, FontStyle.Bold),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlWorkoutTable.Controls.Add(headerLabel);

            int yPosition = 40;
            foreach (var result in exerciseResults)
            {
                // Exercise row
                string setsText = string.Join("-", result.Reps);
                string increaseText = result.WeightIncrease > 0 ? $"+{result.WeightIncrease}kg" : "Artırım Yok";

                // Format string for proper alignment
                string exerciseName = result.ExerciseName.Length > 20 ? result.ExerciseName.Substring(0, 17) + "..." : result.ExerciseName.PadRight(20);
                string currentWeight = $"{result.CurrentWeight}kg".PadRight(12);
                string sets = setsText.PadRight(12);
                string minReps = result.MinReps.ToString().PadRight(9);
                string nextWeight = $"{result.NextWeight}kg".PadRight(12);

                Label exerciseLabel = new Label()
                {
                    Text = $"{exerciseName}  {currentWeight}  {sets}  {minReps}  {nextWeight}  {increaseText}",
                    Location = new Point(10, yPosition),
                    Size = new Size(820, 25),
                    Font = new Font("Consolas", 9),
                    BackColor = yPosition % 60 == 40 ? Color.White : Color.AliceBlue,
                    BorderStyle = BorderStyle.FixedSingle
                };
                pnlWorkoutTable.Controls.Add(exerciseLabel);

                yPosition += 30;
            }

            // Summary
            Label summaryLabel = new Label()
            {
                Text = $"Toplam {exerciseResults.Count} egzersiz tamamlandı - {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}",
                Location = new Point(10, yPosition + 10),
                Size = new Size(820, 25),
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };
            pnlWorkoutTable.Controls.Add(summaryLabel);
        }

        private void PrepareWorkoutData()
        {
            currentWorkoutData = $"================================\n";
            currentWorkoutData += $"İDMAN TARİHİ: {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}\n";
            currentWorkoutData += $"TOPLAM EGZERSİZ: {exerciseResults.Count}\n";
            currentWorkoutData += $"================================\n\n";

            foreach (var result in exerciseResults)
            {
                currentWorkoutData += $"EGZERSİZ: {result.ExerciseName}\n";
                currentWorkoutData += $"AĞIRLIK: {result.CurrentWeight} kg\n";
                currentWorkoutData += $"SET SAYISI: {result.SetCount}\n";

                for (int i = 0; i < result.Reps.Count; i++)
                {
                    currentWorkoutData += $"  {i + 1}. Set: {result.Reps[i]} tekrar\n";
                }

                currentWorkoutData += $"En Düşük Tekrar: {result.MinReps}\n";

                if (result.WeightIncrease > 0)
                {
                    currentWorkoutData += $"Sonraki Ağırlık: {result.NextWeight} kg (+{result.WeightIncrease} kg)\n";
                }
                else
                {
                    currentWorkoutData += $"Sonraki Ağırlık: {result.NextWeight} kg (Artırım Yok - Tekrar Sayısını Arttır)\n";
                }

                currentWorkoutData += new string('-', 40) + "\n";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "fitness_log.txt";
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName);

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(currentWorkoutData);
                    sw.WriteLine(new string('=', 60));
                    sw.WriteLine();
                }

                MessageBox.Show($"Antrenman kaydedildi!\nDosya konumu: {filePath}", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSave.Enabled = false;
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
                string fileName = "fitness_log.txt";
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FitnessTracker");
                string filePath = Path.Combine(folderPath, fileName);

                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);

                    Form historyForm = new Form()
                    {
                        Text = "Antrenman Geçmişi",
                        Size = new Size(700, 600),
                        StartPosition = FormStartPosition.CenterParent
                    };

                    TextBox txtHistory = new TextBox()
                    {
                        Multiline = true,
                        ReadOnly = true,
                        ScrollBars = ScrollBars.Vertical,
                        Dock = DockStyle.Fill,
                        Font = new Font("Consolas", 9),
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

        private decimal CalculateWeightIncrease(int minReps)
        {
            if (minReps <= 5)
                return 0m;  // No weight increase for 5 reps or less
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

    // Helper class for exercise results
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