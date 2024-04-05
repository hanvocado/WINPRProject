﻿using System.Windows;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for TopicView.xaml
    /// </summary>
    public partial class TopicView : Window
    {
        public TopicView()
        {
            InitializeComponent();
        }

        private void TechnologyTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TechnologiesList.IsOpen = true;
        }
    }
}
