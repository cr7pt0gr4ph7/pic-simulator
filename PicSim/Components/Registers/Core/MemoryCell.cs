﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Registers
{
    /// <summary>
    /// A readable and writable memory cell.
    /// </summary>
    public class MemoryCell : IRegister
    {
        private byte m_value;

        public byte Value
        {
            get { return m_value; }
            set
            {
                if (value != m_value) {
                    m_value = value;
                    OnRegisterChanged(value);
                }
            }
        }

        private void OnRegisterChanged(byte newValue)
        {
            var handler = RegisterChanged;
            if (handler != null) {
                handler(this, new Notifications.RegisterChangedEventArgs(newValue));
            }
        }

        public event EventHandler<Notifications.RegisterChangedEventArgs> RegisterChanged;
    }
}
