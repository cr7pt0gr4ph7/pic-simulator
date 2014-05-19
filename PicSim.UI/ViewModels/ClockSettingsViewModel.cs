using PicSim.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Applications;
using System.Waf.Foundation;
using System.Windows.Input;

namespace PicSim.UI.ViewModels
{
	public class ClockSettingsViewModel : Model
	{
		private readonly ClockSettingsWindow m_ClockSettingsWindow;

		public ClockSettingsViewModel()
		{
			OpenSettingsCommand = new DelegateCommand(() => {
				m_ClockSettingsWindow.Show();
			});

			ClickSet = new DelegateCommand(() => {
				m_ClockSettingsWindow.Close();
			});

			m_ClockSettingsWindow = new ClockSettingsWindow();
			m_ClockSettingsWindow.DataContext = this;
		}


		public ICommand OpenSettingsCommand { get; private set; }
		public ICommand ClickSet { get; private set; }
	}

	
}
