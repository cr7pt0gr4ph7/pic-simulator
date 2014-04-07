using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Storage
{
    /// <summary>
    /// A memory view that uses an indirect addressing scheme when
    /// the absolute address specified is zero (0x00).
    /// </summary>
    public class IndirectingMemoryView : IMemoryView
    {
        private Func<byte> m_addressGetter;
        private IMemoryView m_underlying;

        public IndirectingMemoryView(Func<byte> indirectAddressGetter, IMemoryView underlying)
        {
            Ensure.ArgumentNotNull(indirectAddressGetter, "indirectAddressGetter");
            Ensure.ArgumentNotNull(underlying, "underlying");

            m_addressGetter = indirectAddressGetter;
            m_underlying = underlying;
        }

        private byte GetMappedAddress(byte address)
        {
            if (address == 0x00) return m_addressGetter();
            return address;
        }

        public byte this[byte address]
        {
            get { return m_underlying[GetMappedAddress(address)]; }
            set { m_underlying[GetMappedAddress(address)] = value; }
        }
    }
}
