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
        private readonly ProgramCounterViewModel m_programCounter;

        private readonly RegisterViewModel m_workingRegister;
        private readonly SpecialRegistersViewModel m_specialRegisters;

        private readonly EditorViewModel m_editor;
        private readonly HelpViewModel m_help;
        private readonly LogViewModel m_log;

		private readonly FrequencyViewModel m_frequencyController;
		private readonly RS232ViewModel m_RS232Controller;

        public MainWindowViewModel(IView view, IFileDialogService fileDialogService, SimulatorModel simulatorModel)
            : base(view)
        {
            Ensure.ArgumentNotNull(fileDialogService, "fileDialogService");
            Ensure.ArgumentNotNull(simulatorModel, "simulatorModel");

            m_log = new LogViewModel();
            m_help = new HelpViewModel();
            m_editor = new EditorViewModel();

            m_simulatorModel = simulatorModel;
            m_memoryTable = new MemoryTableViewModel(m_simulatorModel.Processor.DebugMemoryView, 8);
            m_programCounter = new ProgramCounterViewModel(m_simulatorModel.Processor.ProgramCounter);

            m_workingRegister = new RegisterViewModel(m_simulatorModel.Processor.WorkingRegister);
            m_specialRegisters = new SpecialRegistersViewModel(m_simulatorModel.Processor);

			m_frequencyController = new FrequencyViewModel(m_simulatorModel.Processor);
			m_RS232Controller = new RS232ViewModel(m_simulatorModel.Processor, (Components.Communication.RS232) m_simulatorModel.Processor.Communication.Connection);

            LoadFileCommand = new DelegateCommand(() => {
                // Stop the simulator if it is running
                m_simulatorModel.HardReset();

                var lstFileType = new FileType("Listing files", ".LST");
                var openResult = fileDialogService.ShowOpenFileDialog(lstFileType);

                if (!openResult.IsValid)
                {
                    // The user cancelled the dialog, so no action is taken.
                    return;
                }

                // Try to load the specified file
                simulatorModel.LoadFile(openResult.FileName);

                // TOOD Remove this
                DoRequeryCommands();
            });

            StartCommand = new DelegateCommand(() => m_simulatorModel.Start(), () => m_simulatorModel.File != null);
            StopCommand = new DelegateCommand(() => m_simulatorModel.Stop(), () => m_simulatorModel.File != null);
            StartStopCommand = new DelegateCommand(() => m_simulatorModel.ToggleRunning(), () => m_simulatorModel.File != null);
            ResetCommand = new DelegateCommand(() => m_simulatorModel.HardReset(), () => m_simulatorModel.File != null);

            RequeryCommands = new DelegateCommand(() => {
                DoRequeryCommands();
                CommandManager.InvalidateRequerySuggested();
            });
        }

        public DelegateCommand LoadFileCommand { get; private set; }

        public DelegateCommand StartCommand { get; private set; }
        public DelegateCommand StopCommand { get; private set; }
        public DelegateCommand StartStopCommand { get; private set; }
        public DelegateCommand RequeryCommands { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }

        public SimulatorModel Simulator
        {
            get { return m_simulatorModel; }
        }

        public MemoryTableViewModel MemoryTable
        {
            get { return m_memoryTable; }
        }

        public ProgramCounterViewModel PC
        {
            get { return m_programCounter; }
        }

        public RegisterViewModel W
        {
            get { return m_workingRegister; }
        }

        public SpecialRegistersViewModel SpecialRegisters
        {
            get { return m_specialRegisters; }
        }

        public EditorViewModel Editor
        {
            get { return m_editor; }
        }

        public HelpViewModel Help
        {
            get { return m_help; }
        }

        public LogViewModel Log
        {
            get { return m_log; }
        }

		public FrequencyViewModel FrequencyController
		{
			get { return m_frequencyController; }
		}

		public RS232ViewModel RS232Controller
		{
			get { return m_RS232Controller; }
		}

        private void DoRequeryCommands()
        {
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
            StartStopCommand.RaiseCanExecuteChanged();
        }
    }
}
