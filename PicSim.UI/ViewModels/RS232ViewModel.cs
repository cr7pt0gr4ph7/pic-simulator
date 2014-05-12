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
using System.Windows.Data;

namespace PicSim.UI.ViewModels
{
    public class RS232ViewModel : Model
    {
        private readonly Processor m_processor;
		private readonly RS232 m_RS232;
        private readonly RS232SettingsWindow m_RS232SettingsWindow;

		private bool m_activ;

		private readonly CollectionView m_ports;
		private string m_portEntry;

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

			//Test
		   IList<PhoneBookEntry> list = new List<PhoneBookEntry>();
		   list.Add(new PhoneBookEntry("test"));
		   list.Add(new PhoneBookEntry("test2"));
		   m_ports = new CollectionView(list);

		   m_RS232SettingsWindow = new RS232SettingsWindow();
		   m_RS232SettingsWindow.DataContext = this;
        }

        public ICommand OpenSettingCommand { get; private set; }
        public ICommand ClickSet { get; private set; }

		public bool Activ{
			get{return m_activ;}
			set { SetProperty(ref m_activ, value); }
		}

		public CollectionView Ports
		{
			get { return m_ports; }
		}

		public string Port
		{
			get { return m_portEntry; }
			set
			{
				if (m_portEntry == value) return;
				m_portEntry = value;
			}
		}

		// TEmp
		private class PhoneBookEntry
		{
			public string Name { get; set; }
			public PhoneBookEntry(string name)
			{
				Name = name;
			}
		}

	}
    
}
