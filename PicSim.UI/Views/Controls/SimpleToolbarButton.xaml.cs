using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PicSim.UI.Views
{
    /// <summary>
    /// Interaction logic for SimpleToolbarButton.xaml
    /// </summary>
    public partial class SimpleToolbarButton : UserControl
    {
        private string _Text = "";
        public string BText { get { return _Text; } set { _Text = value; txt.Text = value; } }

        public ImageSource _Source;
        public ImageSource BSource { get { return _Source; } set { _Source = value; img.Source = value; } }

        #region BCommand Dependency Property

        public ICommand BCommand
        {
            get { return (ICommand)GetValue(BCommandProperty); }
            set { SetValue(BCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BCommandProperty =
            DependencyProperty.Register("BCommand", typeof(ICommand), typeof(SimpleToolbarButton), new PropertyMetadata());

        #endregion

        public SimpleToolbarButton()
        {
            InitializeComponent();
        }
    }

}
