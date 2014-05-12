using PicSim.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class EditorViewModel : Model
    {
        private readonly ObservableSet<int> m_breakpoints;
        private bool m_showLineNumbers;

        public EditorViewModel()
        {
            m_breakpoints = new ObservableSet<int>();
            m_showLineNumbers = true;
        }

        public ICollection<int> Breakpoints
        {
            get { return m_breakpoints; }
        }

        public bool ShowLineNumbers
        {
            get { return m_showLineNumbers; }
            set { SetProperty(ref m_showLineNumbers, value); }
        }
    }
}
