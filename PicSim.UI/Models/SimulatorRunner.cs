using PicSim.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PicSim.UI.Models
{
    public class SimulatorRunner
    {
        private readonly Processor m_processor;
        private readonly Dispatcher m_dispatcher;
        private bool m_isRunning;

        public SimulatorRunner(Processor processor, Dispatcher dispatcher)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            Ensure.ArgumentNotNull(dispatcher, "dispatcher");

            m_processor = processor;
            m_dispatcher = dispatcher;
        }

        private void InternalRun()
        {
            if (m_isRunning) {
                m_processor.Step();
                m_dispatcher.BeginInvoke(InternalRun);
            }
        }

        public void Start()
        {
            // HACK Stupid implementation
            if (m_isRunning) {
                return;
            }
            m_isRunning = true;
            m_dispatcher.BeginInvoke(InternalRun);
        }
    }
}
