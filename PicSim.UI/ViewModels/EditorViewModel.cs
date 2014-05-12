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
        private bool m_showLineNumbers;

        public EditorViewModel()
        {
            m_showLineNumbers = true;
        }

        public bool ShowLineNumbers
        {
            get { return m_showLineNumbers; }
            set { SetProperty(ref m_showLineNumbers, value); }
        }
    }
}
