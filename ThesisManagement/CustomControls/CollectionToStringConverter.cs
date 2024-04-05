using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.CustomControls
{
    class CollectionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Student> Students)
            {
                return string.Join(Environment.NewLine, Students.Select(s => s.Name));
            }

            if (value is ObservableCollection<string> collection)
            {
                return string.Join(",", collection); 
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                string[] array = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                ObservableCollection<string> stringList = new ObservableCollection<string>(array);
                return string.Join(Environment.NewLine, stringList);
            }
            return null;
        }
    }

}
