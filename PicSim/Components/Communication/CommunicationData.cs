using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication
{
    class CommunicationData :
        IRegister
    {
        private byte _value;

        public byte Value
        {
            get
            {
                return (byte)(_value & 0xFF);
            }
            set
            {
                _value = value;
            }
        }

        public byte getLowerNibble()
        {
            return (byte)((_value & 0xF) | 0x30);
        }

        public byte getUpperNibble()
        {
            return (byte)(((_value & 0xF0) >> 4) | 0x30);
        }

        public void setLowerNibble(byte data)
        {
            _value = (byte)((data & 0x0F) | (_value & 0xF0));
        }

        public void setUpperNibble(byte data)
        {
            _value = (byte)((data & 0x0F) << 4 | (_value & 0x0F));
        }

        public event EventHandler<Notifications.RegisterChangedEventArgs> RegisterChanged;
    }
}
