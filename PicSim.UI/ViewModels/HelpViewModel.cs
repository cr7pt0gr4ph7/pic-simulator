using System;
using System.Diagnostics;
using System.IO;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Input;

namespace PicSim.UI.ViewModels
{
    public class HelpViewModel
    {
        public HelpViewModel()
        {
            ShowHelpPdf = new DelegateCommand(() => {
                try
                {
                    var curAssembly = typeof(HelpViewModel).Assembly;
                    var applicationPath = Path.GetDirectoryName(curAssembly.Location);

                    var helpPdfPath = Path.Combine(applicationPath, "PicSim - Help.pdf");
                    Process.Start(helpPdfPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Anzeigen des Hilfe-PDFs:\n\n" + ex.ToString(), "PicSim - Hilfe", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public ICommand ShowHelpPdf { get; private set; }
    }
}
