using PicSim.Execution;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class SpecialRegistersViewModel : Model
    {
        public SpecialRegistersViewModel(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");

            PortA = new IORegisterViewModel(processor.Registers.TRISA, processor.Registers.PORTA);
            PortB = new IORegisterViewModel(processor.Registers.TRISB, processor.Registers.PORTB);
        }

        public IORegisterViewModel PortA { get; private set; }
        public IORegisterViewModel PortB { get; private set; }
    }
}
