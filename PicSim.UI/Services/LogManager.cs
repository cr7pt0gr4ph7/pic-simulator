using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Utils;

namespace PicSim.UI.Services
{
	public class LogManager
	{
		/// <summary>
		/// This event is raised every time a message is logged.
		/// </summary>
		public static event EventHandler<LogEventArgs> Logged;

		private static void OnLogged(LogEventArgs e)
		{
			Logged.RaiseIfNotNull(this, e);
		}

		/// <summary>
		/// Log a message.s
		/// </summary>
		/// <param name="message"></param>
		public static void Log(string message)
		{
			OnLogged(new LogEventArgs(message));
		}

		public class LogEventArgs : EventArgs
		{
			private readonly string _message;

			public LogEventArgs(string message)
			{
				Ensure.ArgumentNotNull(message, "message");
				_message = message;
			}

			public string Message
			{
				get { return _message; }
			}
		}
	}
}
