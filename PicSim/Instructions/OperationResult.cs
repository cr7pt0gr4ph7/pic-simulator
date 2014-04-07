using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// The result of an arithmetic operation that has been
    /// performed using the methods of the <see cref="Operations"/> class.
    /// </summary>
    public struct OperationResult
    {
        private readonly byte m_value;
        private readonly bool m_carry;
        private readonly bool m_digitCarry;

        public OperationResult(byte value, bool carry, bool digitCarry)
        {
            m_value = value;
            m_carry = carry;
            m_digitCarry = digitCarry;
        }

        public byte Value
        {
            get { return m_value; }
        }

        public bool Carry
        {
            get { return m_carry; }
        }

        public bool DigitCarry
        {
            get { return m_digitCarry; }
        }

        public bool Zero
        {
            get { return m_value == 0; }
        }
    }
}
