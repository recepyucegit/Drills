using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliskanlikTakibi
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HabitForm());
        }
    }

    public partial class HabitForm : Form
    {
        private Dictionary<DateTime, DailyHabits> habits;
        private DateTime currentMonth;
        private Panel calendarPanel;
        private Label monthLabel;
        private Panel taskPanel;
        private DateTime selectedDate;

        public HabitForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.habits = new Dictionary<DateTime, DailyHabits>();
            this.currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.selectedDate = DateTime.Today;

            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Text = "Alışkanlık Takibi";
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9);
            this.Name = "HabitForm";

            SetupCustomComponents();
            CreateCalendar();

            this.ResumeLayout(false);
        }

        private void SetupCustomComponents()
        {
            // Ana panel
            var mainPanel = new TableLayoutPanel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.ColumnCount = 2;
            mainPanel.RowCount = 1;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));

            // Sol panel - Takvim
            var leftPanel = new Panel();
            leftPanel.BackColor = Color.White;
            leftPanel.Margin = new Padding(10);
            leftPanel.BorderStyle = BorderStyle.FixedSingle;

            // Başlık paneli
            var headerPanel = new Panel();
            headerPanel.Height = 50;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = Color.FromArgb(70, 130, 180);

            // Ay navigasyonu
            var prevButton = new Button();
            prevButton.Text = "◀";
            prevButton.Size = new Size(40, 30);
            prevButton.Location = new Point(10, 10);
            prevButton.BackColor = Color.White;
            prevButton.FlatStyle = FlatStyle.Flat;
            prevButton.Click += PrevMonth_Click;

            monthLabel = new Label();
            monthLabel.TextAlign = ContentAlignment.MiddleCenter;
            monthLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            monthLabel.ForeColor = Color.White;
            monthLabel.Dock = DockStyle.Fill;

            var nextButton = new Button();
            nextButton.Text = "▶";
            nextButton.Size = new Size(40, 30);
            nextButton.Location = new Point(headerPanel.Width - 50, 10);
            nextButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nextButton.BackColor = Color.White;
            nextButton.FlatStyle = FlatStyle.Flat;
            nextButton.Click += NextMonth_Click;

            headerPanel.Controls.Add(prevButton);
            headerPanel.Controls.Add(monthLabel);
            headerPanel.Controls.Add(nextButton);

            // Takvim paneli
            calendarPanel = new Panel();
            calendarPanel.Dock = DockStyle.Fill;
            calendarPanel.BackColor = Color.White;

            leftPanel.Controls.Add(headerPanel);
            leftPanel.Controls.Add(calendarPanel);

            // Sağ panel - Görevler
            taskPanel = new Panel();
            taskPanel.BackColor = Color.FromArgb(245, 245, 245);
            taskPanel.Margin = new Padding(10);
            taskPanel.BorderStyle = BorderStyle.FixedSingle;

            mainPanel.Controls.Add(leftPanel);
            mainPanel.Controls.Add(taskPanel);
            this.Controls.Add(mainPanel);

            UpdateMonthLabel();
            CreateTaskPanel();
        }

        private void PrevMonth_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(-1);
            UpdateMonthLabel();
            CreateCalendar();
        }

        private void NextMonth_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(1);
            UpdateMonthLabel();
            CreateCalendar();
        }

        private void CreateCalendar()
        {
            calendarPanel.Controls.Clear();

            var startOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
            var firstDayOfWeek = (int)startOfMonth.DayOfWeek;
            if (firstDayOfWeek == 0) firstDayOfWeek = 7;

            // Gün başlıkları
            string[] dayNames = { "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt", "Paz" };
            for (int i = 0; i < 7; i++)
            {
                var dayLabel = new Label();
                dayLabel.Text = dayNames[i];
                dayLabel.Size = new Size(80, 30);
                dayLabel.Location = new Point(i * 85 + 10, 10);
                dayLabel.TextAlign = ContentAlignment.MiddleCenter;
                dayLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dayLabel.BackColor = Color.FromArgb(230, 230, 230);
                dayLabel.BorderStyle = BorderStyle.FixedSingle;
                calendarPanel.Controls.Add(dayLabel);
            }

            // Gün butonları
            int row = 1;
            int col = firstDayOfWeek - 1;

            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayDate = new DateTime(currentMonth.Year, currentMonth.Month, day);
                var dayButton = new Button();
                dayButton.Text = day.ToString();
                dayButton.Size = new Size(80, 70);
                dayButton.Location = new Point(col * 85 + 10, row * 75 + 40);
                dayButton.Tag = dayDate;
                dayButton.FlatStyle = FlatStyle.Flat;
                dayButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dayButton.Cursor = Cursors.Hand;

                // Bugünü vurgula
                if (dayDate.Date == DateTime.Today)
                {
                    dayButton.BackColor = Color.FromArgb(100, 149, 237);
                    dayButton.ForeColor = Color.White;
                }
                else if (dayDate.Date == selectedDate.Date)
                {
                    dayButton.BackColor = Color.FromArgb(70, 130, 180);
                    dayButton.ForeColor = Color.White;
                }
                else
                {
                    dayButton.BackColor = Color.White;
                    dayButton.ForeColor = Color.Black;
                }

                // Tamamlanan günler için tik işareti
                if (habits.ContainsKey(dayDate.Date) && habits[dayDate.Date].IsCompleted)
                {
                    dayButton.Text += "\n✓";
                    dayButton.BackColor = Color.FromArgb(144, 238, 144);
                    dayButton.ForeColor = Color.FromArgb(34, 139, 34);
                }

                dayButton.Click += DayButton_Click;
                calendarPanel.Controls.Add(dayButton);

                col++;
                if (col > 6)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private void DayButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            selectedDate = (DateTime)button.Tag;
            CreateCalendar();
            CreateTaskPanel();
        }

        private void CreateTaskPanel()
        {
            taskPanel.Controls.Clear();

            // Başlık
            var titleLabel = new Label();
            titleLabel.Text = $"Görevler - {selectedDate.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"))}";
            titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titleLabel.Location = new Point(10, 10);
            titleLabel.Size = new Size(250, 30);
            titleLabel.ForeColor = Color.FromArgb(70, 130, 180);
            taskPanel.Controls.Add(titleLabel);

            // Görevler için alan
            if (!habits.ContainsKey(selectedDate.Date))
            {
                habits[selectedDate.Date] = new DailyHabits();
            }

            var dailyHabits = habits[selectedDate.Date];

            for (int i = 0; i < 3; i++)
            {
                var taskIndex = i;

                // Görev etiketi
                var taskLabel = new Label();
                taskLabel.Text = $"Görev {i + 1}:";
                taskLabel.Location = new Point(10, 60 + i * 80);
                taskLabel.Size = new Size(60, 20);
                taskLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                taskPanel.Controls.Add(taskLabel);

                // Görev text kutusu
                var taskTextBox = new TextBox();
                taskTextBox.Location = new Point(10, 80 + i * 80);
                taskTextBox.Size = new Size(200, 25);
                taskTextBox.Font = new Font("Segoe UI", 10);
                taskTextBox.Text = dailyHabits.Tasks[i] ?? "";
                taskTextBox.Tag = taskIndex;
                taskTextBox.TextChanged += TaskTextBox_TextChanged;
                taskPanel.Controls.Add(taskTextBox);

                // Tamamlandı checkbox
                var completedCheckBox = new CheckBox();
                completedCheckBox.Text = "Tamamlandı ✓";
                completedCheckBox.Location = new Point(10, 110 + i * 80);
                completedCheckBox.Size = new Size(120, 20);
                completedCheckBox.Font = new Font("Segoe UI", 9);
                completedCheckBox.Checked = dailyHabits.TaskCompleted[i];
                completedCheckBox.ForeColor = Color.FromArgb(34, 139, 34);
                completedCheckBox.Tag = taskIndex;
                completedCheckBox.CheckedChanged += CompletedCheckBox_CheckedChanged;
                taskPanel.Controls.Add(completedCheckBox);
            }

            // Günlük durum
            var statusLabel = new Label();
            statusLabel.Text = dailyHabits.IsCompleted ? "🎉 Bu gün tamamlandı!" : "⏳ Devam et!";
            statusLabel.Location = new Point(10, 300);
            statusLabel.Size = new Size(200, 30);
            statusLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            statusLabel.ForeColor = dailyHabits.IsCompleted ? Color.Green : Color.Orange;
            taskPanel.Controls.Add(statusLabel);

            // İstatistikler
            var completedDays = habits.Values.Count(h => h.IsCompleted);
            var totalDays = habits.Count;
            var statsLabel = new Label();
            statsLabel.Text = $"📊 Toplam: {completedDays}/{totalDays} gün tamamlandı";
            statsLabel.Location = new Point(10, 340);
            statsLabel.Size = new Size(200, 20);
            statsLabel.Font = new Font("Segoe UI", 10);
            statsLabel.ForeColor = Color.FromArgb(70, 130, 180);
            taskPanel.Controls.Add(statsLabel);

            // Başarı yüzdesi
            if (totalDays > 0)
            {
                var percentage = (completedDays * 100) / totalDays;
                var percentLabel = new Label();
                percentLabel.Text = $"🏆 Başarı: %{percentage}";
                percentLabel.Location = new Point(10, 365);
                percentLabel.Size = new Size(200, 20);
                percentLabel.Font = new Font("Segoe UI", 10);
                percentLabel.ForeColor = percentage >= 80 ? Color.Green :
                                       percentage >= 50 ? Color.Orange : Color.Red;
                taskPanel.Controls.Add(percentLabel);
            }
        }

        private void TaskTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            var index = (int)textBox.Tag;

            if (!habits.ContainsKey(selectedDate.Date))
            {
                habits[selectedDate.Date] = new DailyHabits();
            }

            habits[selectedDate.Date].Tasks[index] = textBox.Text;
        }

        private void CompletedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            var index = (int)checkBox.Tag;

            if (!habits.ContainsKey(selectedDate.Date))
            {
                habits[selectedDate.Date] = new DailyHabits();
            }

            habits[selectedDate.Date].TaskCompleted[index] = checkBox.Checked;
            habits[selectedDate.Date].UpdateCompletionStatus();

            CreateCalendar();
            CreateTaskPanel();
        }

        private void UpdateMonthLabel()
        {
            monthLabel.Text = currentMonth.ToString("MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));
        }
    }

    public class DailyHabits
    {
        public string[] Tasks { get; set; }
        public bool[] TaskCompleted { get; set; }
        public bool IsCompleted { get; set; }

        public DailyHabits()
        {
            Tasks = new string[3];
            TaskCompleted = new bool[3];
            IsCompleted = false;
        }

        public void UpdateCompletionStatus()
        {
            IsCompleted = TaskCompleted.Any(completed => completed);
        }
    }
}