using PicSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Applications;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class StackDisplayViewModel : Model
    {
        private readonly Stack m_stack;
        private IEnumerable<StackEntry> m_entries;

        public StackDisplayViewModel(Stack stack)
        {
            Ensure.ArgumentNotNull(stack, "stack");
            m_stack = stack;

            m_stack.StackChanged += (sender, e) => {
                UpdateStack();
            };
            UpdateStack();
        }

        private void UpdateStack()
        {
            StackEntries = m_stack.Entries.Select((adr, idx) => {
                bool isActive = (idx == m_stack.CurrentIndex);
                return new StackEntry(adr, "", isActive);
            }).Reverse().ToArray();
        }

        public IEnumerable<StackEntry> StackEntries
        {
            get { return m_entries; }
            set { SetProperty(ref m_entries, value); }
        }

        public class StackEntry
        {
            public StackEntry(ushort address, string label, bool isActive)
            {
                this.Address = address;
                this.Label = label;
                this.IsActive = isActive;
            }

            public ushort Address { get; private set; }
            public string Label { get; private set; }
            public bool IsActive { get; private set; }
        }
    }
}
