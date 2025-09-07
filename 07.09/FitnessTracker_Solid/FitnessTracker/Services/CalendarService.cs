using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace FitnessTracker.Services
{
    public class CalendarService : ICalendarService
    {
        public void UpdateCalendar(Panel calendarPanel, Label monthYearLabel, DateTime currentMonth, Dictionary<DateTime, bool> workoutDays, Action<DateTime> onDayClick)
        {
            calendarPanel.Controls.Clear();
            monthYearLabel.Text = currentMonth.ToString("MMMM yyyy", new CultureInfo("tr-TR"));

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
                calendarPanel.Controls.Add(dayLabel);
            }

            DateTime firstDayOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
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

                    DateTime currentDate = new DateTime(currentMonth.Year, currentMonth.Month, day);
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

                    if (onDayClick != null)
                        dayBox.Click += (s, e) => onDayClick(currentDate);

                    calendarPanel.Controls.Add(dayBox);
                    day++;
                    if (day > lastDayOfMonth.Day) break;
                }
                y += 40;
                if (day > lastDayOfMonth.Day) break;
            }
        }

        public void ChangeMonth(ref DateTime currentMonth, int delta)
        {
            currentMonth = currentMonth.AddMonths(delta);
        }
    }
}
