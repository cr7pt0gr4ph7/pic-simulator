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

namespace PicSim.UI.ViewModels
{
    public class FrequencyViewModel : Model
    {
        private readonly Processor m_processor;
        private readonly FrequencyControllerWindow m_frequencyControllerWindow;

        private int m_currentFrequency;
        private int m_newFrequency;

        public FrequencyViewModel(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            m_processor = processor;

            CurrentFrequency = m_processor.Clock.Frequency;

            OpenSettingCommand = new DelegateCommand(() => {
                NewFrequency = m_processor.Clock.Frequency;
                m_frequencyControllerWindow.Show();
            });

            ClickSet = new DelegateCommand(() => {
                m_processor.Clock.Frequency = CurrentFrequency = NewFrequency;
                m_frequencyControllerWindow.Close();
            });

            m_frequencyControllerWindow = new FrequencyControllerWindow();
            m_frequencyControllerWindow.DataContext = this;
        }

        public int CurrentFrequency
        {
            get { return m_currentFrequency; }
            private set { SetProperty(ref m_currentFrequency, value); }
        }

        public int NewFrequency
        {
            get { return m_newFrequency; }
            set { SetProperty(ref m_newFrequency, value); }
        }

        public ICommand OpenSettingCommand { get; private set; }
        public ICommand ClickSet { get; private set; }
    }
}
