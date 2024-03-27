using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {
        private readonly IScheduleRepository _scheduleRepo;

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        } 
        
        private int thesisId;
        public int ThesisId
        {
            get { return thesisId; }
            set
            {
                thesisId = value;
                OnPropertyChanged(nameof(ThesisId));
            }
        }

        private DateTime from;
        public DateTime From
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        private DateTime to;
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged(nameof(To));
            }
        }

        private string eventName;
        public string EventName
        {
            get { return eventName; }
            set
            {
                eventName = value;
                OnPropertyChanged(nameof(EventName));
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private ScheduleInfo selectedSchedule;
        public ScheduleInfo SelectedSchedule
        {
            get { return selectedSchedule; }
            set
            {
                selectedSchedule = value;
                OnPropertyChanged(nameof(SelectedSchedule));
            }
        }

        private IEnumerable<ScheduleInfo> scheduleInfos;
        public IEnumerable<ScheduleInfo> ScheduleInfos
        {
            get { return scheduleInfos; }
            set
            {
                scheduleInfos = value;
                OnPropertyChanged(nameof(ScheduleInfos));
            }
        }

        public ScheduleViewModel()
        {
            _scheduleRepo = new ScheduleRepository();
            selectedSchedule = new();
            ScheduleInfos = _scheduleRepo.GetScheduleInfos(thesisId);
        }

    }
}
