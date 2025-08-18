using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfToDoApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private string filePath = "tasks.json";
        public MainWindow()
        {
            InitializeComponent();

            // Manuel olarak event bağla
            txtTask.GotFocus += TxtTask_GotFocus;
            txtTask.LostFocus += TxtTask_LostFocus;

            LoadTasks();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        // Placeholder: odaklanınca metni temizle
        private void TxtTask_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTask.Text == "Görev Giriniz...")
            {
                txtTask.Text = "";
                txtTask.Foreground = Brushes.Black;
            }
        }

        private void TxtTask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTask.Text))
            {
                txtTask.Text = "Görev Giriniz...";
                txtTask.Foreground = Brushes.Gray;
            }
        }


        // Görev ekleme
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string taskText = txtTask.Text.Trim();
            if (string.IsNullOrEmpty(taskText) || taskText == "Görev Giriniz...")
                return;

            TaskItem task = new TaskItem
            {
                Content = taskText,
                IsImportant = chkImportant.IsChecked == true,
                IsDone = false,
                StartTime = DateTime.Now
            };

            ListBoxItem item = new ListBoxItem
            {
                Tag = task,
                Padding = new Thickness(5),
                FontWeight = FontWeights.SemiBold,
                Foreground = task.IsImportant ? Brushes.Red : Brushes.Black
            };

            UpdateItemContent(item);

            if (task.IsImportant)
                listBoxToDo.Items.Insert(0, item);
            else
                listBoxToDo.Items.Add(item);

            txtTask.Text = "Görev Giriniz...";
            txtTask.Foreground = Brushes.Gray;
            chkImportant.IsChecked = false;

            SaveTasks();
        }

        // Görev tamamlandığında
        private void listBoxToDo_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listBoxToDo.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)listBoxToDo.SelectedItem;
                listBoxToDo.Items.Remove(item);

                TaskItem task = (TaskItem)item.Tag;
                task.IsDone = true;
                item.Tag = task;

                listBoxDone.Items.Add(item);
                SaveTasks();
            }
        }

        // Timer ile süre güncelle
        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (ListBoxItem item in listBoxToDo.Items)
                UpdateItemContent(item);
        }

        // Liste öğesi içeriğini ve süreyi güncelle
        private void UpdateItemContent(ListBoxItem item)
        {
            TaskItem task = (TaskItem)item.Tag;
            TimeSpan elapsed = DateTime.Now - task.StartTime;
            string elapsedStr = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                              elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
            item.Content = $"{task.Content} [{elapsedStr}]";

            // Süreye göre renk
            if (elapsed.TotalHours >= 1)
                item.Background = Brushes.LightCoral;
            else
                item.Background = Brushes.LightGreen;
        }

        // JSON kaydet
        private void SaveTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            foreach (ListBoxItem item in listBoxToDo.Items)
                tasks.Add((TaskItem)item.Tag);

            foreach (ListBoxItem item in listBoxDone.Items)
                tasks.Add((TaskItem)item.Tag);

            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // JSON yükle
        private void LoadTasks()
        {
            if (!File.Exists(filePath)) return;

            string json = File.ReadAllText(filePath);
            var tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json);

            foreach (var task in tasks)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Tag = task,
                    Padding = new Thickness(5),
                    FontWeight = FontWeights.SemiBold,
                    Foreground = task.IsImportant ? Brushes.Red : Brushes.Black
                };

                if (task.IsDone)
                    listBoxDone.Items.Add(item);
                else
                    listBoxToDo.Items.Add(item);

                UpdateItemContent(item);
            }
        }
    }

    // Görev modeli
  
}
