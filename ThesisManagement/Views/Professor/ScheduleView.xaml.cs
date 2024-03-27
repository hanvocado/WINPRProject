using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            ScheduleViewModel dataContext = this.DataContext as ScheduleViewModel ?? new ScheduleViewModel();
            if (appointment != null)
            {
                dataContext.SelectedSchedule.From = appointment.StartTime;
                dataContext.SelectedSchedule.To = appointment.EndTime;
                dataContext.SelectedSchedule.EventName = appointment.Subject;
                dataContext.SelectedSchedule.Location = appointment.Location;
                dataContext.SelectedSchedule.ThesisId = dataContext.ThesisId;
                _scheduleRepo.Add(dataContext.SelectedSchedule);
            }
        }

    }
}
