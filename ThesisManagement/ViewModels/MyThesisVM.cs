using Microsoft.Win32;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class MyThesisVM : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;
        private readonly IThesisRepository _thesisRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IProfessorRepository _professorRepo;
        private readonly DialogService _dialogService;
        private string destinationPath;
        private string appDirectory;
        private string studentFilePath;

        private Topic topic;
        public Topic Topic
        {
            get { return topic; }
            set { topic = value; OnPropertyChanged(nameof(Topic)); }
        }

        private Thesis thesis;
        public Thesis Thesis
        {
            get { return thesis; }
            set
            {
                thesis = value;
                if (thesis != null)
                {
                    Topic = thesis.Topic;
                    Thesis.Students = _studentRepo.GetStudent(Thesis.Id)?.ToList() ?? new List<Student>();
                    Evaluation = Thesis.Evaluation ?? string.Empty;
                    var student = Thesis.Students.FirstOrDefault(st => st.Id == SessionInfo.UserId);
                    Score = (student != null) ? student.Score : 0;
                    UpdateEvaluations();
                }
                OnPropertyChanged(nameof(Thesis));
            }
        }

        private string evaluation;
        public string Evaluation
        {
            get { return evaluation; }
            set { evaluation = value; OnPropertyChanged(nameof(Evaluation)); }
        }

        private float? score;
        public float? Score
        {
            get { return score; }
            set { score = value; OnPropertyChanged(nameof(Score)); }
        }

        public bool IsTopicApproved
        {
            get
            {
                return this.Thesis.TopicStatus == Variable.StatusTopic.Approved;
            }
        }

        private Stream docStream;
        public Stream DocumentStream
        {
            get
            {
                return docStream;
            }
            set
            {
                docStream = value;
                OnPropertyChanged(nameof(DocumentStream));
            }
        }

        public ICommand MakeEvaluationCommand { get; set; }
        public ICommand UploadFileCommand { get; set; }

        public MyThesisVM()
        {
            _thesisRepo = new ThesisRepository();
            _professorRepo = new ProfessorRepository();
            _topicRepo = new TopicRepository();
            _dialogService = new DialogService();
            _studentRepo = new StudentRepository();
            appDirectory = SessionInfo.BinDirectory;

            if (SessionInfo.Role == Role.Student)
                Thesis ??= _studentRepo.GetThesis(SessionInfo.UserId) ?? new Thesis();

            MakeEvaluationCommand = new ViewModelCommand(ExecuteMakeEvaluationCommand);
            UploadFileCommand = new ViewModelCommand(ExecuteUploadFileCommand);
        }

        private void UpdateEvaluations()
        {
            if (Thesis != null && !string.IsNullOrEmpty(Thesis.File))
            {
                //Update professor evaluation and student score
                Evaluation = thesis.Evaluation;
                studentFilePath = Path.Combine(appDirectory, Thesis.File);
                DocumentStream = new FileStream(studentFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
        }

        private void ExecuteMakeEvaluationCommand(object obj)
        {
            bool? confirmEvaluate = _dialogService.ShowDialog(Message.Notification, Message.EvaluateNotification);
            if (confirmEvaluate == true)
            {
                thesis.Evaluation = evaluation;
                bool successAddScore = true;
                foreach (var student in Thesis.Students)
                {
                    successAddScore = _studentRepo.Update(student);
                    if (!successAddScore)
                        break;
                }
                var successUpdateThesis = _thesisRepo.Update(thesis);
                ShowMessage(successAddScore && successUpdateThesis, Message.UpdateSuccess, Message.UpdateFailed);
            }
        }

        private void ExecuteUploadFileCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                //Uploaded file name
                string fileName = openFileDialog.FileName;
                if (Path.GetExtension(fileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    string userFileName = SessionInfo.UserId + Path.GetFileName(fileName);

                    //Storage file name
                    destinationPath = Path.Combine(appDirectory, userFileName);
                    File.Copy(fileName, destinationPath, true);
                    DocumentStream = new FileStream(destinationPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                    //Update database
                    thesis.File = userFileName;
                    var success = _thesisRepo.Update(thesis);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }
                else
                {
                    _dialogService.ShowDialog(Message.ErrorNotification, Message.UploadFileFailed);
                } 
                    
            }
        }
    }
}
