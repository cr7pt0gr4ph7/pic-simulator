using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicSim.UI.Views
{
    /// <summary>
    /// Interaction logic for LEDandSwitches.xaml
    /// </summary>
    public partial class LEDandSwitches : UserControl
    {
        #region Header Dependency Property    

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(LEDandSwitches), new PropertyMetadata());

        #endregion

        public LEDandSwitches()
        {
            InitializeComponent();
        }
    }
}
