using PicSim.UI.Models;
using PicSim.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Applications;
using System.Waf.Applications.Services;
using System.Windows.Input;

namespace PicSim.UI.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly SimulatorModel m_simulatorModel;
        private readonly MemoryTableViewModel m_memoryTable;

        public MainWindowViewModel(IView view, IFileDialogService fileDialogService, SimulatorModel simulatorModel)
            : base(view)
        {
            Ensure.ArgumentNotNull(fileDialogService, "fileDialogService");
            Ensure.ArgumentNotNull(simulatorModel, "simulatorModel");

            m_simulatorModel = simulatorModel;
            m_memoryTable = new MemoryTableViewModel(m_simulatorModel.Processor.DebugMemoryView, 8);

            LoadFileCommand = new LoadFileCommandImpl(fileDialogService, simulatorModel);
        }

        public ICommand LoadFileCommand { get; private set; }

        public SimulatorModel Simulator
        {
            get { return m_simulatorModel; }
        }

        public MemoryTableViewModel MemoryTable
        {
            get { return m_memoryTable; } 
        }

        private class LoadFileCommandImpl : ICommand
        {
            private readonly IFileDialogService m_fileDialogService;
            private readonly IFileLoaderService m_fileLoaderService;

            public LoadFileCommandImpl(IFileDialogService fileDialogService, IFileLoaderService fileLoaderService)
            {
                Ensure.ArgumentNotNull(fileDialogService, "fileDialogService");
                Ensure.ArgumentNotNull(fileLoaderService, "fileLoaderService");

                this.m_fileDialogService = fileDialogService;
                this.m_fileLoaderService = fileLoaderService;
            }

            public void Execute(object parameter)
            {
                var lstFileType = new FileType("Listing files", ".LST");
                var openResult = m_fileDialogService.ShowOpenFileDialog(lstFileType);

                if (!openResult.IsValid) {
                    // The user cancelled the dialog, so no action is taken.
                    return;
                }

                // Try to load the specified file
                m_fileLoaderService.LoadFile(openResult.FileName);
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

        }
    }
}
