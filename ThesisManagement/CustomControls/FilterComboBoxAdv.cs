using Microsoft.Xaml.Behaviors;
using Syncfusion.Windows.Tools.Controls;
using System.Windows.Data;
using ThesisManagement.Models;

namespace ThesisManagement.CustomControls
{
    public class FilterComboBoxAdv : TargetedTriggerAction<ComboBoxAdv>
    {
        protected override void Invoke(object parameter)
        {
            CollectionView items = (CollectionView)CollectionViewSource.GetDefaultView(Target.ItemsSource);
            items.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(Target.Text))
                {
                    return true;
                }
                else
                {
                    if ((o as Student).Name.Contains(Target.Text, StringComparison.OrdinalIgnoreCase) || (o as Student).Id.Contains(Target.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            });
            items.Refresh();
        }
    }
}
