using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ThesisManagement.CustomControls
{
    public partial class TemplateButton : UserControl
    {
        public static readonly DependencyProperty DefaultColorProperty =
            DependencyProperty.Register("DefaultColor", typeof(Brush), typeof(TemplateButton), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty HoverColorProperty =
           DependencyProperty.Register("HoverColor", typeof(Brush), typeof(TemplateButton), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty TitleProperty =
           DependencyProperty.Register("Title", typeof(object), typeof(TemplateButton));

        public static readonly DependencyProperty ButtonWidthProperty =
           DependencyProperty.Register("ButtonWidth", typeof(int), typeof(TemplateButton), new PropertyMetadata(80));

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

        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public int ButtonWidth
        {
            get { return (int)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        public TemplateButton()
        {
            InitializeComponent();
        }
    }
}
