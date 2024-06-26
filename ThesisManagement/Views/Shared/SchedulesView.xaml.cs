﻿using Syncfusion.UI.Xaml.Scheduler;
using System.Windows;
using System.Windows.Controls;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.ViewModels;
using ThesisManagement.Views.Student;

namespace ThesisManagement.Views.Shared
{
    /// <summary>
    /// Interaction logic for SchedulesView.xaml
    /// </summary>
    public partial class SchedulesView : UserControl
    {
        private readonly IScheduleRepository _scheduleRepo;
        public SchedulesView()
        {
            _scheduleRepo = new ScheduleRepository();
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi");
            this.Schedule.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            this.Schedule.AppointmentEditorClosing += Schedule_AppointmentEditorClosing;
        }

        private void Schedule_AppointmentEditorClosing(object sender, AppointmentEditorClosingEventArgs e)
        {
            var appointment = e.Appointment as ScheduleAppointment;
            var success = false;
            if (appointment == null) return;

            ScheduleVM? scheduleVM = this.DataContext as ScheduleVM ?? new ScheduleVM();
            ScheduleInfo schedule = new ScheduleInfo
            {
                From = appointment.StartTime,
                To = appointment.EndTime,
                EventName = appointment.Subject,
                Location = appointment.Location,
                ThesisId = scheduleVM.ThesisId,
                Note = appointment.Notes
            };
            switch (e.Action)
            {
                case AppointmentEditorAction.Add:
                    success = _scheduleRepo.Add(schedule);
                    break;
                case AppointmentEditorAction.Edit:
                    schedule.Id = (int)appointment.Id;
                    _scheduleRepo.Update(schedule);
                    break;
                case AppointmentEditorAction.Delete:
                    schedule.Id = (int)appointment.Id;
                    success = _scheduleRepo.Delete(schedule.Id);
                    break;
            }
            scheduleVM.ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
            scheduleVM.Load();
        }

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            if (SessionInfo.Role == Role.Student)
            {
                e.Cancel = true;
                var appointment = e.Appointment as ScheduleAppointment;
                if (appointment != null)
                {
                    var scheduleVM = new ScheduleVM
                    {
                        Id = (int)appointment.Id,
                        From = appointment.StartTime,
                        To = appointment.EndTime,
                        EventName = appointment.Subject,
                        Location = appointment.Location,
                        Note = appointment.Notes
                    };
                    var scheduleView = new ScheduleDetailsView { DataContext = scheduleVM };
                    scheduleView.Owner = Window.GetWindow(this);
                    scheduleView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    scheduleView.Show();
                }
                return;
            }
        }
    }
}
