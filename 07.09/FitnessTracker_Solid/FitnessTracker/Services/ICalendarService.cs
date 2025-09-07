using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FitnessTracker.Services
{
    public interface ICalendarService
    {
        void UpdateCalendar(Panel calendarPanel, Label monthYearLabel, DateTime currentMonth, Dictionary<DateTime, bool> workoutDays, Action<DateTime> onDayClick);
        void ChangeMonth(ref DateTime currentMonth, int delta);
    }
}
