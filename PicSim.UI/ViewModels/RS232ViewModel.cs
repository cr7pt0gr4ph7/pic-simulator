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
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Windows;
using PicSim.Components.Communication.Interfaces;

namespace PicSim.UI.ViewModels
{
    public class RS232ViewModel : Model
    {
        private const string NONE = "<none>";
        private readonly Processor m_processor;
        private readonly CommunicationManager m_commManager;
        private readonly RS232SettingsWindow m_RS232SettingsWindow;

        private readonly ObservableCollection<string> m_availablePorts  = new ObservableCollection<string>();
        private string m_selectedPort;

        public RS232ViewModel(Processor processor, CommunicationManager commManager)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            Ensure.ArgumentNotNull(commManager, "commManager");

            m_processor = processor;
            m_commManager = commManager;
            m_selectedPort = NONE;

            OpenSettingsCommand = new DelegateCommand(() => {
                m_availablePorts.Clear();

                m_availablePorts.Add(NONE);

                foreach (var port in SerialPort.GetPortNames())
                    m_availablePorts.Add(port);

                m_RS232SettingsWindow.Show();
            });

            ClickSet = new DelegateCommand(() => {
                m_commManager.SetUnderlying(() => {
                    try
                    {
                        if (SelectedPort == NONE)
                            return NullCommunication.Instance;
                        else
                            return new RS232(SelectedPort);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception while trying to open port:\n\n" + ex.ToString(), "PicSim - COM-Port", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                });
                m_RS232SettingsWindow.Close();
            });

            m_RS232SettingsWindow = new RS232SettingsWindow();
            m_RS232SettingsWindow.DataContext = this;
        }

        public ICommand OpenSettingsCommand { get; private set; }
        public ICommand ClickSet { get; private set; }

        public IEnumerable<string> AvailablePorts
        {
            get { return m_availablePorts; }
        }

        public string SelectedPort
        {
            get { return m_selectedPort; }
            set { SetProperty(ref m_selectedPort, value); }
        }
    }

}
