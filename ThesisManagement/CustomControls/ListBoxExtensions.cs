using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace ThesisManagement.CustomControls
{
    public static class ListBoxExtensions
    {
        public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.RegisterAttached(
            "SelectedItems",
            typeof(IList),
            typeof(ListBoxExtensions),
            new PropertyMetadata(null, OnSelectedItemsChanged));

        public static void SetSelectedItems(DependencyObject element, IList value)
        {
            element.SetValue(SelectedItemsProperty, value);
        }

        public static IList GetSelectedItems(DependencyObject element)
        {
            return (IList)element.GetValue(SelectedItemsProperty);
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listBox = d as ListBox;
            if (listBox != null)
            {
                listBox.SelectionChanged -= ListBox_SelectionChanged;
                listBox.SelectionChanged += ListBox_SelectionChanged;
            }
        }

        private static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedItems = GetSelectedItems(listBox);
            foreach (var item in e.AddedItems)
            {
                if (selectedItems != null)
                    selectedItems.Add(item);
            }
            foreach (var item in e.RemovedItems)
            {
                if (selectedItems != null)
                    selectedItems.Remove(item);
            }
        }
    }
}
