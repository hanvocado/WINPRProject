using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Controls;
using ThesisManagement.Models;
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
            if (appointment == null) return;

            ScheduleViewModel? scheduleVM = this.DataContext as ScheduleViewModel ?? new ScheduleViewModel();
            ScheduleInfo schedule = new ScheduleInfo
            {
                From = appointment.StartTime,
                To = appointment.EndTime,
                EventName = appointment.Subject,
                Location = appointment.Location,
                ThesisId = scheduleVM.ThesisId,
                Note = scheduleVM.Note
            };
            switch (e.Action)
            {
                case AppointmentEditorAction.Add:
                    _scheduleRepo.Add(schedule);
                    break;
                case AppointmentEditorAction.Edit:
                    _scheduleRepo.Update(schedule);
                    break;
                case AppointmentEditorAction.Delete:
                    _scheduleRepo.Delete(schedule.Id);
                    break;
            }

            ((ScheduleViewModel)this.DataContext).CountUpcomingSchedules = _scheduleRepo.CountUpcomingSchedules(scheduleVM.ThesisId);
        }

    }
}
