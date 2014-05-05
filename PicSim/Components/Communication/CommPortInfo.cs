using PicSim.Components.Notifications;
using PicSim.Components.Registers;
using PicSim.Utils;
using System;

namespace PicSim.Components.Communication
{
    /// <summary>
    /// This class models a communcation port (PORTA, PORTB, ...) which has a value register and a tristate register.
    /// </summary>
    public class CommPortInfo
    {
        private readonly IORegisterImpl m_valueRegister;
        private readonly TrisRegisterImpl m_trisRegister;

        private byte m_trisValue;
        private byte m_realInput;
        private byte m_dataLatches;
        private byte m_cachedValue;

        public CommPortInfo()
        {
            m_trisRegister = new TrisRegisterImpl(this);
            m_valueRegister = new IORegisterImpl(this);

            Refresh();
        }

        private void Refresh()
        {
            var lastValue = m_cachedValue;
            m_cachedValue = (byte)((RealInput & TrisValue) | (DataLatches & TrisValue.Complement()));

            if (m_cachedValue != lastValue)
            {
                m_valueRegister.RaiseRegisterChanged();
            }
        }

        private byte TrisValue
        {
            get { return m_trisValue; }
            set { m_trisValue = value; Refresh(); }
        }

        public byte RealInput
        {
            get { return m_realInput; }
            set { m_realInput = value; Refresh(); }
        }

        private byte DataLatches
        {
            get { return m_dataLatches; }
            set { m_dataLatches = value; Refresh(); }
        }

        public IRegister ValueRegister
        {
            get { return m_valueRegister; }
        }

        public IRegister TrisRegister
        {
            get { return m_trisRegister; }
        }

        private class IORegisterImpl : IRegister
        {
            private readonly CommPortInfo m_outer;

            public IORegisterImpl(CommPortInfo outer)
            {
                m_outer = outer;
            }

            public byte Value
            {
                get { return m_outer.m_cachedValue; }
                set { m_outer.DataLatches = value; }
            }

            public void RaiseRegisterChanged()
            {
                RegisterChanged.RaiseIfNotNull(this, new RegisterChangedEventArgs(Value));
            }

            public event EventHandler<RegisterChangedEventArgs> RegisterChanged;
        }

        private class TrisRegisterImpl : IRegister
        {
            private readonly CommPortInfo m_outer;

            public TrisRegisterImpl(CommPortInfo outer)
            {
                m_outer = outer;
            }

            public byte Value
            {
                get { return m_outer.TrisValue; }
                set { m_outer.TrisValue = value; }
            }

            public event EventHandler<RegisterChangedEventArgs> RegisterChanged;
        }
    }
}
