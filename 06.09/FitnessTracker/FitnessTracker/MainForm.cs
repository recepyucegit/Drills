using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace FitnessTracker
{
    public partial class MainForm : Form
    {
        private TextBox txtExerciseName;
        private NumericUpDown numSets;
        private NumericUpDown numWeight;
        private TextBox[] txtReps;
        private Label[] lblSetLabels;
        private Button btnCalculate;
        private Button btnSave;
        private Button btnViewHistory;
        private Label lblResult;
        private Label lblNextWeight;
        private string currentWorkoutData = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(500, 650);
            this.Text = "Fitness Takip Uygulaması";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Exercise Name
            Label lblExercise = new Label()
            {
                Text = "Egzersiz Adı:",
                Location = new Point(20, 20),
                Size = new Size(100, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            this.Controls.Add(lblExercise);

            txtExerciseName = new TextBox()
            {
                Location = new Point(130, 18),
                Size = new Size(200, 25),
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(txtExerciseName);

            // Weight
            Label lblWeight = new Label()
            {
                Text = "Ağırlık (kg):",
                Location = new Point(20, 60),
                Size = new Size(100, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            this.Controls.Add(lblWeight);

            numWeight = new NumericUpDown()
            {
                Location = new Point(130, 58),
                Size = new Size(100, 25),
                Font = new Font("Arial", 10),
                DecimalPlaces = 2,
                Increment = 0.25m,
                Maximum = 1000m,
                Minimum = 0m
            };
            this.Controls.Add(numWeight);

            // Sets
            Label lblSets = new Label()
            {
                Text = "Set Sayısı:",
                Location = new Point(20, 100),
                Size = new Size(100, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            this.Controls.Add(lblSets);

            numSets = new NumericUpDown()
            {
                Location = new Point(130, 98),
                Size = new Size(100, 25),
                Font = new Font("Arial", 10),
                Minimum = 1,
                Maximum = 4,
                Value = 3
            };
            numSets.ValueChanged += NumSets_ValueChanged;
            this.Controls.Add(numSets);

            // Reps for each set
            txtReps = new TextBox[4];
            lblSetLabels = new Label[4];

            for (int i = 0; i < 4; i++)
            {
                lblSetLabels[i] = new Label()
                {
                    Text = $"{i + 1}. Set Tekrar:",
                    Location = new Point(20, 140 + (i * 35)),
                    Size = new Size(100, 20),
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Visible = i < 3
                };
                this.Controls.Add(lblSetLabels[i]);

                txtReps[i] = new TextBox()
                {
                    Location = new Point(130, 138 + (i * 35)),
                    Size = new Size(100, 25),
                    Font = new Font("Arial", 10),
                    Visible = i < 3
                };
                this.Controls.Add(txtReps[i]);
            }

            // Calculate button
            btnCalculate = new Button()
            {
                Text = "Hesapla",
                Location = new Point(50, 290),
                Size = new Size(80, 35),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightBlue,
                UseVisualStyleBackColor = false
            };
            btnCalculate.Click += BtnCalculate_Click;
            this.Controls.Add(btnCalculate);

            // Save button
            btnSave = new Button()
            {
                Text = "Kaydet",
                Location = new Point(140, 290),
                Size = new Size(80, 35),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightGreen,
                UseVisualStyleBackColor = false,
                Enabled = false
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            // View History button
            btnViewHistory = new Button()
            {
                Text = "Geçmiş",
                Location = new Point(230, 290),
                Size = new Size(80, 35),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightYellow,
                UseVisualStyleBackColor = false
            };
            btnViewHistory.Click += BtnViewHistory_Click;
            this.Controls.Add(btnViewHistory);

            // Result labels
            lblResult = new Label()
            {
                Location = new Point(20, 340),
                Size = new Size(450, 60),
                Font = new Font("Arial", 10),
                ForeColor = Color.Blue,
                Text = ""
            };
            this.Controls.Add(lblResult);

            lblNextWeight = new Label()
            {
                Location = new Point(20, 410),
                Size = new Size(450, 80),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red,
                Text = ""
            };
            this.Controls.Add(lblNextWeight);
        }

        private void NumSets_ValueChanged(object sender, EventArgs e)
        {
            int setCount = (int)numSets.Value;

            for (int i = 0; i < 4; i++)
            {
                lblSetLabels[i].Visible = i < setCount;
                txtReps[i].Visible = i < setCount;
                if (!txtReps[i].Visible)
                    txtReps[i].Text = "";
            }
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtExerciseName.Text))
            {
                MessageBox.Show("Lütfen egzersiz adını girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numWeight.Value == 0)
            {
                MessageBox.Show("Lütfen ağırlık değerini girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int setCount = (int)numSets.Value;
            int[] reps = new int[setCount];
            bool hasError = false;

            for (int i = 0; i < setCount; i++)
            {
                if (!int.TryParse(txtReps[i].Text, out reps[i]) || reps[i] <= 0)
                {
                    MessageBox.Show($"Lütfen {i + 1}. set için geçerli tekrar sayısını girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hasError = true;
                    break;
                }
            }

            if (hasError) return;

            // Find minimum reps (worst set)
            int minReps = reps[0];
            for (int i = 1; i < setCount; i++)
            {
                if (reps[i] < minReps)
                    minReps = reps[i];
            }

            // Calculate weight increase based on minimum reps
            decimal weightIncrease = CalculateWeightIncrease(minReps);
            decimal nextWeight = numWeight.Value + weightIncrease;

            // Display current workout info
            string currentInfo = $"Egzersiz: {txtExerciseName.Text}\n";
            currentInfo += $"Mevcut Ağırlık: {numWeight.Value} kg\n";
            currentInfo += "Set Detayları: ";

            for (int i = 0; i < setCount; i++)
            {
                currentInfo += $"{i + 1}.Set: {reps[i]} tekrar";
                if (i < setCount - 1) currentInfo += ", ";
            }

            lblResult.Text = currentInfo;

            // Display next workout recommendation
            string nextInfo = $"Sonraki İdman İçin Önerilen Ağırlık:\n";
            nextInfo += $"{nextWeight} kg (+{weightIncrease} kg artış)\n";
            nextInfo += $"En düşük tekrar sayısı: {minReps} tekrar";

            lblNextWeight.Text = nextInfo;

            // Prepare data for saving
            currentWorkoutData = $"Tarih: {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}\n";
            currentWorkoutData += $"Egzersiz: {txtExerciseName.Text}\n";
            currentWorkoutData += $"Ağırlık: {numWeight.Value} kg\n";
            currentWorkoutData += $"Set Sayısı: {setCount}\n";

            for (int i = 0; i < setCount; i++)
            {
                currentWorkoutData += $"  {i + 1}. Set: {reps[i]} tekrar\n";
            }

            currentWorkoutData += $"En Düşük Tekrar: {minReps}\n";
            currentWorkoutData += $"Sonraki Ağırlık Önerisi: {nextWeight} kg (+{weightIncrease} kg)\n";

            btnSave.Enabled = true;
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
                    sw.WriteLine(new string('-', 50));
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
                        Size = new Size(600, 500),
                        StartPosition = FormStartPosition.CenterParent
                    };

                    TextBox txtHistory = new TextBox()
                    {
                        Multiline = true,
                        ReadOnly = true,
                        ScrollBars = ScrollBars.Vertical,
                        Dock = DockStyle.Fill,
                        Font = new Font("Consolas", 10),
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
            // Weight increase logic based on minimum reps
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
}