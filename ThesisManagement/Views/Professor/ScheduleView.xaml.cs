using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Controls;
using ThesisManagement.Repositories;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : UserControl
    {
        private readonly IScheduleRepository _scheduleRepo;
        public ScheduleView()
        {
            _scheduleRepo = new ScheduleRepository();
            InitializeComponent();
            this.Schedule.AppointmentEditorClosing += Schedule_AppointmentEditorClosing;
        }

        private void Schedule_AppointmentEditorClosing(object sender, AppointmentEditorClosingEventArgs e)
        {
            var appointment = e.Appointment as ScheduleAppointment;
            ScheduleViewModel? scheduleVM = this.DataContext as ScheduleViewModel;
            if (scheduleVM != null && appointment != null)
            {
                var newSchedule = new Models.ScheduleInfo
                {
                    From = appointment.StartTime,
                    To = appointment.EndTime,
                    EventName = appointment.Subject,
                    Location = appointment.Location,
                    ThesisId = scheduleVM.ThesisId
                };
                _scheduleRepo.Add(newSchedule);
                ((ScheduleViewModel)this.DataContext).CountUpcomingSchedules = _scheduleRepo.CountUpcomingSchedules(scheduleVM.ThesisId);
            }
        }

    }
}
