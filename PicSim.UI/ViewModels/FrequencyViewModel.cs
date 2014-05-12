using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Applications;

using PicSim.Execution;
using PicSim.UI.Views;
using System.Windows.Input;

namespace PicSim.UI.ViewModels
{
	public class FrequencyViewModel
	{
		Processor m_processor;


		public FrequencyViewModel(Processor processor)
        {
			m_processor = processor;

			FrequencyControllerWindow frequencyControllerWindow = new FrequencyControllerWindow();

			OpenSettingCommand = new DelegateCommand(() => { frequencyControllerWindow.Show(); });
			ClickSet = new DelegateCommand(() => { });
        }

		public ICommand OpenSettingCommand { get; private set; }
		public ICommand ClickSet { get; private set; }
	}
}
