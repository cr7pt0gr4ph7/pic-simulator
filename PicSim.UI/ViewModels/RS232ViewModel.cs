using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Applications;

using PicSim.Execution;
using PicSim.UI.Views;
using System.Windows.Input;
using System.Waf.Foundation;
using PicSim.Components.Communication;

namespace PicSim.UI.ViewModels
{
    public class RS232ViewModel : Model
    {
        private readonly Processor m_processor;
		private readonly RS232 m_RS232;
        private readonly RS232SettingsWindow m_RS232SettingsWindow;

        public RS232ViewModel(Processor processor, RS232 rs232)
        {
            Ensure.ArgumentNotNull(processor, "processor");
			Ensure.ArgumentNotNull(rs232, "rs232");
            m_processor = processor;
			m_RS232 = rs232;

			OpenSettingCommand = new DelegateCommand(() => {
				m_RS232SettingsWindow.Show();
			});

           ClickSet = new DelegateCommand(() => {
			   m_RS232SettingsWindow.Close();
            });

		   m_RS232SettingsWindow = new RS232SettingsWindow();
		   m_RS232SettingsWindow.DataContext = this;
        }

        public ICommand OpenSettingCommand { get; private set; }
        public ICommand ClickSet { get; private set; }
    }
}
