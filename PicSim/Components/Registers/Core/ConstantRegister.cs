using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Registers
{
    public class ConstantRegister : IRegister
    {
        private readonly byte m_constantValue;

        public ConstantRegister(byte constantValue)
        {
            m_constantValue = constantValue;
        }

        public byte Value
        {
            get { return m_constantValue; }
            set { /* Do nothing */ }
        }

        // NOTE: No notifications will be sent.
        public event EventHandler<Notifications.RegisterChangedEventArgs> RegisterChanged;
    }
}
