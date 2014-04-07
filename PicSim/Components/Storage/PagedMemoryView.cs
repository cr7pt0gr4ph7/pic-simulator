using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Components.Registers;

namespace PicSim.Components.Storage
{
    public class PagedMemoryView : IMemoryView
    {
        private readonly Func<bool> m_bankSelector;
        private readonly IMemoryView m_underlying;

        public PagedMemoryView(Func<bool> bankSelector, IMemoryView underlying)
        {
            Ensure.ArgumentNotNull(bankSelector, "bankSelector");
            Ensure.ArgumentNotNull(underlying, "underlying");

            m_bankSelector = bankSelector;
            m_underlying = underlying;
        }

        private void CheckAddress(byte address)
        {
            if (address >= 0x80) {
                throw new ArgumentOutOfRangeException("address", address, "The value must not have its highest byte set (and thus be smaller than 128 (decimal) / 0x80 (hex)).");
            }
        }

        /// <summary>
        /// Map the specified 7-bit wide address to an 8-bit wide address
        /// by using the bank selector
        /// </summary>
        /// <param name="address">The 7-bit wide address to be mapped.</param>
        /// <returns>An 8-bit wide address.</returns>
        private byte GetMappedAddress(byte address)
        {
            CheckAddress(address);
            return (byte)(GetExtraBits() | address);
        }

        private byte GetExtraBits()
        {
            if (m_bankSelector()) {
                // Bank 1 selected
                return 0x80;
            } else {
                // Bank 0 selected
                return 0x00;
            }
        }

        public byte this[byte address]
        {
            get { return m_underlying[GetMappedAddress(address)]; }
            set { m_underlying[GetMappedAddress(address)] = value; }
        }
    }
}


