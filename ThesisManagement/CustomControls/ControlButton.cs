using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ThesisManagement.CustomControls
{
    public class ControlButton : Button
    {
        public static readonly DependencyProperty DefaultColorProperty =
            DependencyProperty.Register("DefaultColor", typeof(Brush), typeof(ControlButton), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty HoverColorProperty =
           DependencyProperty.Register("HoverColor", typeof(Brush), typeof(ControlButton), new PropertyMetadata(Brushes.Transparent));

        public Brush DefaultColor
        {
            get { return (Brush)GetValue(DefaultColorProperty); }
            set { SetValue(DefaultColorProperty, value); }
        }

        public Brush HoverColor
        {
            get { return (Brush)GetValue(HoverColorProperty); }
            set { SetValue(HoverColorProperty, value); }
        }

        static ControlButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ControlButton), new FrameworkPropertyMetadata(typeof(ControlButton)));
        }
    }
}
