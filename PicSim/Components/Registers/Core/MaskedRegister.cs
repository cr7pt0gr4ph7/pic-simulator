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
            set { m_value = (byte)((m_value & m_mask.Complement()) | (value & m_mask)); }
        }

        public byte RawValue
        {
            get { return m_value; }
            set { m_value = value; }
        }

        public BitVector32 BitVector
        {
            get { return new BitVector32(m_value); }
            set { m_value = (byte)value.Data; }
        }
    }
}
