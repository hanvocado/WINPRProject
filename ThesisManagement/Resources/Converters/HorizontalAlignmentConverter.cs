using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ThesisManagement.Repositories;
using Role = ThesisManagement.Repositories.Role;

namespace ThesisManagement.Resources.Converters
{
    public class HorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter is string panel)
            {
                switch (SessionInfo.Role)
                {
                    case Role.Student:
                        if (panel == "StudentMessage")
                        {
                            return HorizontalAlignment.Right;
                        }
                        else if (panel == "ProfessorMessage")
                        {
                            return HorizontalAlignment.Left;
                        }
                        break;
                    case Role.Professor:
                        if (panel == "StudentMessage")
                        {
                            return HorizontalAlignment.Left;
                        }
                        else if (panel == "ProfessorMessage")
                        {
                            return HorizontalAlignment.Right;
                        }
                        break;
                }
            }    
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
