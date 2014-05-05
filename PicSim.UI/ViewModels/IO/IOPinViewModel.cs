using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class IOPinViewModel : Model
    {
        private readonly IRegister m_trisRegister;
        private readonly IRegister m_valueRegister;
        private readonly byte m_bitOffset;

        public IOPinViewModel(IRegister trisRegister, IRegister valueRegister, byte bitOffset)
        {
            Ensure.ArgumentNotNull(trisRegister, "trisRegister");
            Ensure.ArgumentNotNull(valueRegister, "valueRegister");

            m_trisRegister = trisRegister;
            m_valueRegister = valueRegister;
            m_bitOffset = bitOffset;

            // TODO This raises a property changed event even if the bit we are interested in is not changed (but other bits are changed).
            m_trisRegister.RegisterChanged += (sender, e) => RaisePropertyChanged("Direction");
            m_valueRegister.RegisterChanged += (sender, e) => RaisePropertyChanged("Value");
        }

        public IOPortDirection Direction
        {
            get { return m_trisRegister.GetBit(m_bitOffset).ToIOPortDirection(); }
            set { m_trisRegister.SetBit(m_bitOffset, value.ToBool()); }
        }

        public bool Value
        {
            get { return m_valueRegister.GetBit(m_bitOffset); }
            set { m_valueRegister.SetBit(m_bitOffset, value); }
        }

        public int BitOffset
        {
            get { return m_bitOffset; }
        }
    }
}
