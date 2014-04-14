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
		private string _text = "";

		public LogViewModel()
		{
			Add("Start");
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
	}
}
