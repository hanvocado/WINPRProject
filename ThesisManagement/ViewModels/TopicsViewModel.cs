﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ProfessorTopicsView = ThesisManagement.Views.Professor.TopicsView;
using ProfessorTopicView = ThesisManagement.Views.Professor.TopicView;

namespace ThesisManagement.ViewModels
{
    public class TopicsViewModel : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;

        private ObservableCollection<Topic> topics;

        private Topic selectedTopic;

        public string name;
        public string? category;
        public string? technology;
        public string description;
        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Computer Science", "Web Development", "Data Science", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "SQL", "ASP.NET Core", "Other" };

        public ICommand ProfessorCreateTopic { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public Topic SelectedTopic
        {
            get
            {
                return selectedTopic;
            }
            set
            {
                selectedTopic = value;
                OnPropertyChanged(nameof(SelectedTopic));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
                FilterData();
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
                FilterData();
            }
        }

        public string Technology
        {
            get { return technology; }
            set
            {
                technology = value;
                OnPropertyChanged(nameof(Technology));
                FilterData();
            }
        }

        public ObservableCollection<Topic> Topics
        {
            get { return topics; }
            set
            {
                topics = value;
                OnPropertyChanged(nameof(Topics));
            }
        }

        public TopicsViewModel()
        {
            selectedTopic = new Topic();
            _topicRepo = new TopicRepository();
            Topics = _topicRepo.GetAll();
            ProfessorCreateTopic = new ViewModelCommand(ExecuteProfessorCreateCommand);
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand);
        }

        private void ExecuteProfessorCreateCommand(object sender)
        {
            ProfessorTopicsView topicsView = sender as ProfessorTopicsView;
            ProfessorTopicView topicView = new();
            if (topicsView != null)
            {
                topicsView.DataContext = this;
            }
            topicView.DataContext = this;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteCreateCommand(object obj)
        {
            Topic newTopic = new Topic
            {
                Name = selectedTopic.Name,
                Category = selectedTopic.Category,
                Technology = selectedTopic.Technology,
                Description = selectedTopic.Description
            };

            _topicRepo.Add(newTopic);
            Topics = _topicRepo.GetAll();
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            if (parameter is int selectedId)
            {
                _topicRepo.Delete(selectedId);
            }
            Topics = _topicRepo.GetAll();
        }

        private void FilterData()
        {
            var filteredData = _topicRepo.GetFilteredTopics(Name, Category, Technology);
            Topics = new ObservableCollection<Topic>(filteredData);
        }
    }
}