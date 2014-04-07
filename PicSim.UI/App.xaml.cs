using PicSim.UI.Models;
using PicSim.UI.Services;
using PicSim.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Waf.Applications;
using System.Waf.Presentation.Services;
using System.Windows;

namespace PicSim.UI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.MainWindow = new MainWindow();
            new MainWindowViewModel((IView)MainWindow, new FileDialogService(), new SimulatorModel());

            this.MainWindow.Show();
        }
    }
}
