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
        private readonly Processor m_processor = new Processor();
        private FileModel m_fileModel;

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
    }
}
