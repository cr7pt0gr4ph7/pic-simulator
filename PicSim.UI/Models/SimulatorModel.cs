using NLog;
using PicSim.Components.Communication;
using PicSim.Execution;
using PicSim.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;
using System.Windows.Threading;

namespace PicSim.UI.Models
{
    /// <summary>
    /// This class provides the connection between the backend (the simulator)
    /// and the frontend (the user interface) of the simulator software.
    /// </summary>
    public class SimulatorModel : Model, IFileLoaderService
    {
        private static readonly Logger ms_logger = LogManager.GetCurrentClassLogger();

        private readonly Processor m_processor = new Processor();
        private readonly DispatcherTimer m_timer;
        private FileModel m_fileModel;

        public SimulatorModel()
        {
            m_timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(0.01d)
            };

            m_timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            m_processor.Step();
        }

        /// <summary>
        /// Load the specified listing file into the program, replacing the one that is currently executing, if any.
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFile(string fileName)
        {
            this.File = FileModel.FromFile(fileName);
            m_processor.LoadProgram(m_fileModel.Opcodes);
        }

        public Processor Processor
        {
            get { return m_processor; }
        }

        public FileModel File
        {
            get { return m_fileModel; }
            set { SetProperty(ref m_fileModel, value); }
        }

        public void Start()
        {
            ms_logger.Info("Simulation started");
            m_timer.Start();
        }

        public void Stop()
        {
            ms_logger.Info("Simulation stopped");
            m_timer.Stop();
        }

        public void HardReset()
        {
            ms_logger.Info("Simulation HardReset");
            m_timer.Stop();
            m_processor.HardReset();
        }

        public void SoftReset()
        {
            ms_logger.Info("Simulation SoftReset");
            m_timer.Stop();
            m_processor.SoftReset();
        }

        public void ToggleRunning()
        {
            if (m_timer.IsEnabled)
                Stop();
            else
                Start();
        }
    }
}
