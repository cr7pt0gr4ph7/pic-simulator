using PicSim.Components.Notifications;
using PicSim.Components.Storage;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    // TODO This is most likely inefficient as hell
    public class MemoryTableViewModel
    {
        private readonly Row[] m_rows;
        private readonly int m_columns;

        public MemoryTableViewModel(IMemoryView memoryView, int columns)
        {
            Ensure.ArgumentNotNull(memoryView, "memoryView");
            Ensure.ArgumentNotNegative(columns, "columns");

            // TODO Use the MemorySize property to determine the actual memory size.
            var memorySize = 0x100;
            var rowCount = memorySize / columns;

            if (memorySize % columns != 0) {
                throw new ArgumentException("The memory size must be divisible by the column count.", "columns");
            }

            m_columns = columns;
            m_rows = new Row[rowCount];

            for (int i = 0; i < rowCount; i++) {
                m_rows[i] = new Row(memoryView, (byte)(i * columns), columns);
            }
        }

        private int GetRowForAddress(int address)
        {
            Ensure.ArgumentNotNegative(address, "address");
            return address / m_columns;
        }

        private int GetColumnForAddress(int address)
        {
            Ensure.ArgumentNotNegative(address, "address");
            return address % m_columns;
        }

        private void memoryView_MemoryChanged(object sender, MemoryChangedEventArgs e)
        {
            var cell = Rows[GetRowForAddress(e.Address)][GetColumnForAddress(e.Address)];
            cell.RaiseChanged();
        }

        public Row[] Rows
        {
            get { return m_rows; }
        }

        public class Row
        {
            private readonly string m_header;
            private readonly Cell[] m_cells;

            public Row(IMemoryView memoryView, byte baseAddress, int columns)
            {
                Ensure.ArgumentNotNull(memoryView, "memoryView");

                m_header = baseAddress.ToString("X2");
                m_cells = new Cell[columns];

                for (byte i = 0; i < columns; i++) {
                    byte cellAddress = (byte)(baseAddress | i);
                    m_cells[i] = new Cell(memoryView, cellAddress);
                }
            }

            public string Header
            {
                get { return m_header; }
            }

            public Cell this[int no]
            {
                get { return m_cells[no]; }
            }
        }

        public class Cell : Model
        {
            private readonly IMemoryView m_memoryView;
            private readonly byte m_address;

            public Cell(IMemoryView memoryView, byte address)
            {
                Ensure.ArgumentNotNull(memoryView, "memoryView");

                m_memoryView = memoryView;
                m_address = address;
            }

            internal void RaiseChanged()
            {
                RaisePropertyChanged("Value");
            }

            public byte Address
            {
                get { return m_address; }
            }

            public byte Value
            {
                get { return m_memoryView[m_address]; }
                // set { m_memoryView[m_address] = value; }
            }
        }
    }
}
