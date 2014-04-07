using System.Collections.Specialized;
using PicSim.Utils;

namespace PicSim.Components.Registers
{
    /// <summary>
    /// A register that only allows writes to certain bytes.
    /// </summary>
    public class MaskedRegister : IRegister
    {
        /// <summary>
        /// The bit mask used for masking writes to this register.
        /// Only writes to bits that have the corresponding bit set in the mask are allowed.
        /// </summary>
        private readonly byte m_mask;

        /// <summary>
        /// The raw value of this register.
        /// </summary>
        private byte m_value;

        public MaskedRegister(byte mask)
        {
            this.m_mask = mask;
        }

        public byte Value
        {
            get { return m_value; }
            set { PrivateValue = (byte)((m_value & m_mask.Complement()) | (value & m_mask)); }
        }

        public byte RawValue
        {
            get { return m_value; }
            set { PrivateValue = value; }
        }

        public BitVector32 BitVector
        {
            get { return new BitVector32(m_value); }
            set { PrivateValue = (byte)value.Data; }
        }

        private byte PrivateValue
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

        public event System.EventHandler<Notifications.RegisterChangedEventArgs> RegisterChanged;
    }
}
