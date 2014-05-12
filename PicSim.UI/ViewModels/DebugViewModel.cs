using PicSim.Components;
using PicSim.Components.Notifications;
using PicSim.UI.Helper;
using PicSim.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class DebugViewModel : Model
    {
        private readonly SimulatorModel m_simulatorModel;
        private readonly ObservableSet<int> m_breakpoints;
        private int m_currentLine;

        public DebugViewModel(SimulatorModel simulatorModel)
        {
            Ensure.ArgumentNotNull(simulatorModel, "simulatorModel");

            this.m_breakpoints = new ObservableSet<int>();
            this.m_simulatorModel = simulatorModel;

            // TODO Use weak events for ProgramCounter.ValueChanged
            var programCounter = m_simulatorModel.Processor.ProgramCounter;
            programCounter.ValueChanged += programCounter_ValueChanged;
        }

        void programCounter_ValueChanged(object sender, ValueChangedEventArgs<ushort> e)
        {
            var file = m_simulatorModel.File;
            if (file == null) return;
            CurrentLine = m_simulatorModel.File.GetLineForPCAddress((int)e.Value) + 1 ?? -1;
        }

        public ICollection<int> Breakpoints
        {
            get { return m_breakpoints; }
        }

        public int CurrentLine
        {
            get { return m_currentLine; }
            private set { SetProperty(ref m_currentLine, value); }
        }
    }
}
