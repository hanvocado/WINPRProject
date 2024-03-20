﻿using System.Windows;
using System.Windows.Controls;
<<<<<<< HEAD
=======
using System.Windows.Controls.Primitives;
>>>>>>> main
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for TopicView.xaml
    /// </summary>
    public partial class TopicsView : UserControl
    {
        private TopicView currentTopicView;
        public static bool IsTopicViewOpen { get; set; } = false;
        public TopicsView()
        {
            InitializeComponent();
            TopicListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
        }

        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedItem = listView?.SelectedItem;

            if (selectedItem != null)
            {
                Topic topic = selectedItem as Topic;
                TopicView topicView = new TopicView();

                if (currentTopicView != null && currentTopicView.IsVisible)
                {
<<<<<<< HEAD
                    SelectedTopic = new Topic
                    {
                        Name = topic.Name,
                        Category = topic.Category,
                        Technology = topic.Technology,
                        Description = topic.Description
                    }
                };
                // ShowMessage($"{topicVM.SelectedTopic.Name}, {topicVM.SelectedTopic.Category},{topicVM.SelectedTopic.Technology},{topicVM.SelectedTopic.Description}");
=======
                    currentTopicView.Close();
                }
>>>>>>> main

                this.DataContext = new TopicsViewModel
                {
                    Id = topic.Id,
                    Name = topic.Name,
                    ProfessorId = topic.ProfessorId,
                    StudentId = topic.StudentId,
                    Category = topic.Category,
                    Technology = topic.Technology,
                    Description = topic.Description,
                    Requirement = topic.Requirement,
                    StudentQuantity = topic.StudentQuantity
                };

                topicView.DataContext = this.DataContext;
                topicView.Owner = Application.Current.MainWindow;
                topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                topicView.Show();

                currentTopicView = topicView;
            }
        }

        private void FilterTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TopicListView.Items.Filter = FilterMethod;
        }

        private bool FilterMethod(object obj)
        {
            var topic = (Topic)obj;
            return topic.Name.Contains(FilterTextbox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Window.GetWindow(this).IsActive)
                return;

            var listViewItem = sender as ListViewItem;
            var topic = listViewItem?.DataContext as Topic;
            TopicsViewModel dataContext = this.DataContext as TopicsViewModel ?? new TopicsViewModel();
            if (topic != null)
            {
                dataContext.Id = topic.Id;
                dataContext.ProfessorId = topic.ProfessorId;
                dataContext.StudentId = topic.StudentId;
                dataContext.Name = topic.Name;
                dataContext.Category = topic.Category;
                dataContext.Technology = topic.Technology;
                dataContext.Description = topic.Description;
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (TopicListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in TopicListView.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)TopicListView.ItemContainerGenerator.ContainerFromItem(item);
                    listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            var topic = listViewItem?.DataContext as Topic;
            TopicsViewModel dataContext = this.DataContext as TopicsViewModel ?? new TopicsViewModel();
            if (topic != null)
            {
                dataContext.SelectedTopic = new Topic
                {
                    Id = topic.Id,
                    Name = topic.Name,
                    Category = topic.Category,
                    Technology = topic.Technology,
                    Description = topic.Description
                };
                //MessageBox.Show($"{topicVM.SelectedTopic.Id},{topicVM.SelectedTopic.Name} ,  {topicVM.SelectedTopic.Category} ,  {topicVM.SelectedTopic.Technology} ,  {topicVM.SelectedTopic.Description}");
            }
        }
    }
}
