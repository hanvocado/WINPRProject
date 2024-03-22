using System.Globalization;
using System.Windows.Data;

namespace ThesisManagement.CustomControls
{
    public class StringNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if the input value is a string
            if (value is string stringValue)
            {
                // Return true if the string is not null or empty
                return string.IsNullOrEmpty(stringValue);
            }

            // For non-string values, return false
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed for one-way binding
            throw new NotImplementedException();
        }
    }
}
