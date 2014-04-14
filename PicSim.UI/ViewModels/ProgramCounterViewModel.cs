using PicSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class ProgramCounterViewModel : Model
    {
        private readonly ProgramCounter m_programCounter;

        public ProgramCounterViewModel(ProgramCounter programCounter)
        {
            Ensure.ArgumentNotNull(programCounter, "programCounter");

            // TODO Use weak events for ProgramCounter.ValueChanged
            this.m_programCounter = programCounter;
            this.m_programCounter.ValueChanged += (sender, e) => RaisePropertyChanged("Value");
        }

        public ushort Value
        {
            get { return m_programCounter.Value; }
            set { m_programCounter.Value = value; }
        }
    }
}
