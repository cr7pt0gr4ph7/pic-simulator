using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class IORegisterViewModel : Model
    {
        private readonly IOPinViewModel[] m_pins;
        private string m_caption;

        public IORegisterViewModel(IRegister trisRegister, IRegister valueRegister)
        {
            Ensure.ArgumentNotNull(trisRegister, "trisRegister");
            Ensure.ArgumentNotNull(valueRegister, "valueRegister");

            m_pins = new IOPinViewModel[8];

            for (int i = 0; i < 8; i++)
                m_pins[i] = new IOPinViewModel(trisRegister, valueRegister, (byte)i);
        }

        public string Caption
        {
            get { return m_caption; }
            set { SetProperty(ref m_caption, value); }
        }

        public IReadOnlyList<IOPinViewModel> Pins
        {
            get { return m_pins; }
        }
    }
}
