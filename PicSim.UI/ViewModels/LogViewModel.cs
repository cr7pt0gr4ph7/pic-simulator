using PicSim.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
	public class LogViewModel : Model, IDisposable
	{
		private string _text = "";

		public LogViewModel()
		{
			Add("Start");
			LogManager.Logged += LogManager_Logged;
		}

		public void Dispose()
		{
			LogManager.Logged -= LogManager_Logged;
		}

		public string Text
		{
			get { return _text; }
			private set { SetProperty(ref _text, value); }
		}

		public void Add(string line)
		{
			_text += DateTime.Now.ToString("HH:mm:ss.ffffff") + " - " + line + "\n";
		}

		void LogManager_Logged(object sender, LogManager.LogEventArgs e)
		{
			Add(e.Message);
		}
	}
}
