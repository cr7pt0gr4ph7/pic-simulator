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
	/// Interaction logic for SpecialRegister.xaml
	/// </summary>
	public partial class SpecialRegister : UserControl
	{
		private ChangeMarkTextBlock[] txtPINS;

		public string Caption { get; set; }
		public String[] pinsCaption { get; set; }
		public bool[] pins { get; private set; }

		public SpecialRegister()
		{
			InitializeComponent();

			this.DataContext = this;

			pinsCaption = new String[] {"0", "1", "2", "3", "4", "5", "6", "7"};
			pins = new bool[8];
			txtPINS = new ChangeMarkTextBlock[] { Pin0, Pin1, Pin2, Pin3, Pin4, Pin5, Pin6, Pin7 };

			Caption = "???";
		}


		private void Pin_MouseDown(uint nmbr)
		{
			
		}


		#region Obnoxious Mouse Events

		private void Pin7_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(7);
		}

		private void Pin6_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(6);
		}

		private void Pin5_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(5);
		}

		private void Pin4_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(4);
		}

		private void Pin3_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(3);
		}

		private void Pin2_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(2);
		}

		private void Pin1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(1);
		}

		private void Pin0_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pin_MouseDown(0);
		}

		#endregion
	}
}
