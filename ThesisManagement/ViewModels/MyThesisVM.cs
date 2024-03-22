using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class MyThesisVM : ViewModelBase
    {
        private readonly IThesisRepository _thesisRepo;
        private readonly IStudentRepository _studentRepo;

        private Thesis thesis;

        public Thesis Thesis
        {
            get { return thesis; }
            set { thesis = value; OnPropertyChanged(nameof(Thesis)); }
        }

        public MyThesisVM()
        {
            _thesisRepo = new ThesisRepository();
            _studentRepo = new StudentRepository();
            Thesis = _studentRepo.GetThesis(SessionInfo.UserId) ?? new Thesis();
        }
    }
}
