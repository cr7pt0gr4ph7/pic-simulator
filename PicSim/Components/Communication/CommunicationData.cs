using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication
{
    class CommunicationData : IRegister
    {
        private byte m_value;

        public byte Value
        {
            get { return (byte)(m_value & 0xFF); }
            set { m_value = value; }
        }

        public byte GetLowerNibble()
        {
            return (byte)((m_value & 0xF) | 0x30);
        }

        public byte GetUpperNibble()
        {
            return (byte)(((m_value & 0xF0) >> 4) | 0x30);
        }

        public void SetLowerNibble(byte data)
        {
            m_value = (byte)((data & 0x0F) | (m_value & 0xF0));
        }

        public void SetUpperNibble(byte data)
        {
            m_value = (byte)((data & 0x0F) << 4 | (m_value & 0x0F));
        }

        public event EventHandler<Notifications.RegisterChangedEventArgs> RegisterChanged;
    }
}
