using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    public struct ByteValue : IEquatable<ByteValue>
    {
        private readonly byte m_data;

        public ByteValue(byte data)
        {
            m_data = data;
        }

        public byte Data
        {
            get { return m_data; }
        }

        #region Operators

        public static ByteValue operator &(ByteValue first, ByteValue second)
        {
            return new ByteValue((byte)(first.Data & second.Data));
        }

        public static ByteValue operator |(ByteValue first, ByteValue second)
        {
            return new ByteValue((byte)(first.Data | second.Data));
        }

        public static ByteValue operator ^(ByteValue first, ByteValue second)
        {
            return new ByteValue((byte)(first.Data ^ second.Data));
        }

        public static ByteValue operator +(ByteValue first, ByteValue second)
        {
            return new ByteValue((byte)(first.Data + second.Data));
        }

        public static ByteValue operator -(ByteValue first, ByteValue second)
        {
            return new ByteValue((byte)(first.Data - second.Data));
        }

        public static ByteValue operator <<(ByteValue first, int amout)
        {
            return new ByteValue((byte)((first.Data << amout) & 0xFF));
        }

        public static ByteValue operator >>(ByteValue first, int amout)
        {
            return new ByteValue((byte)((first.Data >> amout) & 0xFF));
        }

        public static ByteValue operator ~(ByteValue argument)
        {
            return new ByteValue((byte)(argument.Data ^ 0xFF));
        }

        #endregion

        #region Equality operators

        public bool Equals(ByteValue other)
        {
            return (this.Data == other.Data);
        }

        public override bool Equals(object obj)
        {
            return (obj is ByteValue) && Equals((ByteValue)obj);
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public static bool operator ==(ByteValue first, ByteValue second)
        {
            return (first.Data == second.Data);
        }

        public static bool operator !=(ByteValue first, ByteValue second)
        {
            return (first.Data != second.Data);
        }

        #endregion

        #region Conversion operators

        public static implicit operator byte(ByteValue value)
        {
            return value.Data;
        }


        public static implicit operator ByteValue(byte value)
        {
            return new ByteValue(value);
        }

        #endregion

        public override string ToString()
        {
            return m_data.ToString();
        }
    }
}
