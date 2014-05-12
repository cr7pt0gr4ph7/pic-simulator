using NLog;
using NLog.Config;
using NLog.Targets;
using PicSim.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class LogViewModel : Model
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly MemoryEventTarget m_logTarget;
        private string m_text = "";

        public LogViewModel()
        {
            m_logTarget = new MemoryEventTarget();
            m_logTarget.EventReceived += logTarget_EventReceived;
            SimpleConfigurator.ConfigureForTargetLogging(m_logTarget);

            logger.Info("Started");
        }

        public string Text
        {
            get { return m_text; }
            private set { SetProperty(ref m_text, value); }
        }

        void logTarget_EventReceived(string message)
        {
            Text += message + "\n";
        }

        private class MemoryEventTarget : TargetWithLayout
        {
            public event Action<string> EventReceived;

            /// <summary>
            /// Notifies listeners about new event
            /// </summary>
            /// <param name="logEvent">The logging event.</param>
            protected override void Write(LogEventInfo logEvent)
            {
                var handler = EventReceived;
                if (handler != null)
                {
                    handler(Layout.Render(logEvent));
                }
            }
        }
    }
}
