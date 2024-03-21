using System.Windows;
using System.Windows.Controls;
using ThesisManagement.Repositories;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RegisterTopicView : Window
    {
        private readonly IStudentRepository _studentRepo;
        public RegisterTopicView()
        {
            InitializeComponent();
            _studentRepo = new StudentRepository();
        }

        private void CheckComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as HandyControl.Controls.CheckComboBox;
            var currentStudent = _studentRepo.GetStudent(SessionInfo.UserId);
            if (comboBox != null)
            {
                var topicsViewModel = comboBox.DataContext as TopicsViewModel;
                if (topicsViewModel != null)
                {
                    topicsViewModel.SelectedStudents.Clear();
                    topicsViewModel.SelectedStudents.Add(currentStudent);
                    foreach (var item in comboBox.SelectedItems)
                    {
                        topicsViewModel.SelectedStudents.Add(item as Models.Student);
                    }

                }
            }
        }
    }
}
