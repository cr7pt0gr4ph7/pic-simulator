using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace PicSim.UI.Behaviors
{
	public class HideWindowOnCloseBehavior : Behavior<Window>
	{
		protected override void OnAttached()
		{
			AssociatedObject.Closing += AssociatedObject_Closing;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.Closing -= AssociatedObject_Closing;
		}

		void AssociatedObject_Closing(object sender, CancelEventArgs e)
		{
			AssociatedObject.Hide();
			e.Cancel = true;
		}
	}
}
