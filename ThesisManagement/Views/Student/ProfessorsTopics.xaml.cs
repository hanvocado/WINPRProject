﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for ProfessorsTopics.xaml
    /// </summary>
    public partial class ProfessorsTopics : UserControl
    {
        private RegisterTopicView currenTopicView;

        public ProfessorsTopics()
        {
            InitializeComponent();
        }
        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedItem = listView?.SelectedItem;

            if (selectedItem != null)
            {
                Topic topic = selectedItem as Topic;
                RegisterTopicView registerTopic = new RegisterTopicView();

                if (currenTopicView != null && currenTopicView.IsVisible)
                {
                    currenTopicView.Close();
                }

                var vm = new TopicsViewModel
                {
                    SelectedTopic = new Topic
                    {
                        Professor = new Models.Professor
                        {
                            Name = topic.Professor.Name
                        },
                        Id = topic.Id,
                        Name = topic.Name,
                        ProfessorId = topic.ProfessorId,
                        StudentId = topic.StudentId,
                        Category = topic.Category,
                        Technology = topic.Technology,
                        Description = topic.Description,
                        Requirement = topic.Requirement,
                        StudentQuantity = topic.StudentQuantity,
                        Function = topic.Function
                    }
                };
                this.DataContext = vm;
                Trace.WriteLine("Lấy danh sách sv chưa đki thesis nào và sv bị từ chối");
                foreach (var std in vm.Students)
                {
                    Trace.WriteLine($"{std.Name}");
                }
                var currentUser = vm.Students.FirstOrDefault(s => s.Id == SessionInfo.UserId);
                vm.Students.Remove(currentUser);
                vm.SelectedStudents.Add(currentUser);
                vm.SelectedStudentNames = currentUser.Name;

                registerTopic.DataContext = this.DataContext;
                registerTopic.Owner = Application.Current.MainWindow;
                registerTopic.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                registerTopic.Show();

                currenTopicView = registerTopic;
            }
        }


    }
}
