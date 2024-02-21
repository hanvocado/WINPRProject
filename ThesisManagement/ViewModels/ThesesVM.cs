using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class ThesesVM : ViewModelBase
    {
        private string _file { get; set; }
        private float _score { get; set; }  
        private DateTime _start { get; set; }
        private DateTime _end { get; set; }

        public string File
        {
            get => _file;
            set
            {
                _file = value;
                OnPropertyChanged(nameof(File));
            }    
        } 

        public float Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            } 
                
        }    


        public ThesesVM() { }
    }
}
