using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Components;
using PicSim.Components.Registers;
using PicSim.Components.Storage;
using PicSim.Instructions;
using PicSim.Components.Communication;
using PicSim.Components.Interrupts;

namespace PicSim.Execution
{
    public partial class Processor : IResetListener
    {
        public Processor()
        {
            this.Communication = new CommunicationManager();
            this.Communication.SetUnderlying(() => {
                var useExtSerial = true;
                var commConnection = useExtSerial ? (ICommunication)new RS232("COM3") : new MockCommunication();
                return commConnection;
            });

            this.Decoder = new InstructionDecoder() {
                ops = new InstructionOps() { Processor = this }
            };

            this.Watchdog = new Watchdog(this);
            this.Clock = new VirtualClock();
            this.ProgramCounter = new ProgramCounter();

            // Initialize the program stack
            this.Stack = new Stack();

            // Initialize the working register
            this.WorkingRegister = new MemoryCell();

            // Initialize the registers
            //     See "Section 4.2 Data Memory Organization", p.12..18

            this.Registers = new RegistersView(ProgramCounter, Communication);

            // Initialize the virtual memory layout
            //     See "Figure 4-2 Register File Map", p.12

            var memory = new IRegister[0x100];
            var constZero = new ConstantRegister(0x00);

            memory[0x00] = memory[0x80] = constZero;

            memory[0x01] = Registers.TMR0;
            memory[0x81] = Registers.Option;

            memory[0x02] = memory[0x82] = Registers.PCL;
            memory[0x03] = memory[0x83] = Registers.Status;
            memory[0x04] = memory[0x84] = Registers.FSR;

            memory[0x05] = Registers.PORTA;
            memory[0x85] = Registers.TRISA;

            memory[0x06] = Registers.PORTB;
            memory[0x86] = Registers.TRISB;

            memory[0x07] = memory[0x87] = constZero;

            memory[0x08] = Registers.EEDATA;
            memory[0x88] = Registers.EECON1;

            memory[0x09] = Registers.EEADR;
            memory[0x89] = Registers.EECON2;

            memory[0x0A] = memory[0x8A] = Registers.PCLATH;
            memory[0x0B] = memory[0x8B] = Registers.INTCON;

            for (int i = 0x0C; i <= 0x2F; i++)
            {
                // Initialize the General Purpose registers (SRAM)
                memory[i] = memory[i + 0x80] = new MemoryCell();
            }

            for (int i = 0x30; i <= 0x7F; i++)
            {
                // Initialize the unimplemented data memory locations
                memory[i] = memory[i + 0x80] = constZero;
            }

            // Setup the memory, taking into account:
            //   - Indirect addressing using the INDF / FSR registers (via IndirectingMemoryView)
            //   - Memory paging (via PagedMemoryView)
            //   - Registers mirrored to both banks and register to absolute address mapping (via VirtualMemoryView)

            var virtualMemoryView = new VirtualMemoryView(memory);

            this.Memory = new PagedMemoryView(
                () => Registers.Status.RP0,
                new IndirectingMemoryView(
                    () => Registers.FSR.Value,
                    virtualMemoryView
                )
            );

            // Export the virtual memory view for debugging purposes
            this.DebugMemoryView = virtualMemoryView;

            this.InterruptManager = new InterruptManager(this);
        }

        /// <summary>
        /// This Method will reset the Processor
        /// </summary>
        private void Reset()
        {
            Registers.PCL.Value = 0;
            Registers.PCLATH.Value = 0;

            Registers.Option.Value = 0xFF;
            Registers.TRISA.Value = 0x1F;
            Registers.TRISB.Value = 0xFF;

        }

        public void HardReset()
        {
            Registers.Status.Value = 0x18;
            Registers.PORTA.Value = 0x00;
            Registers.PORTB.Value = 0x00;
            Registers.EEDATA.Value = 0x00;
            Registers.EEADR.Value = 0x00;
            Registers.INTCON.Value = 0x00;

            Registers.EECON1.Value = 0x00;


        }

        public void SoftReset()
        {

        }

        #region Debug interface

        /// <summary>
        /// A view of the entire working memory of this processor.
        /// This view does not support indirection via <c>INDF</c> and <c>FSR</c>,
        /// or pagination using the <c>RP0</c> bit of <c>STATUS</c>.
        /// </summary>
        public VirtualMemoryView DebugMemoryView { get; private set; }

        #endregion

        #region ProgramCounter, Stack & ProgramMemory

        /// <summary>
        /// The component managing the program counter of this processor.
        /// </summary>
        public ProgramCounter ProgramCounter { get; private set; }

        /// <summary>
        /// The component managing the call stack of this processor.
        /// </summary>
        public Stack Stack { get; private set; }

        /// <summary>
        /// The array containing the bytecode for the program that is being executed by this processor.
        /// </summary>
        public ushort[] ProgramMemory { get; private set; }

        #endregion

        public CommunicationManager Communication { get; private set; }

        public InstructionDecoder Decoder { get; private set; }

        public Watchdog Watchdog { get; private set; }

        public InterruptManager InterruptManager { get; private set; }

        public VirtualClock Clock { get; private set; }

        #region Registers, Memory & Working register (W)

        /// <summary>
        /// Provides named access to all special function registers.
        /// </summary>
        public RegistersView Registers { get; private set; }

        /// <summary>
        /// Provides indexed access to all registers.
        /// </summary>
        public IMemoryView Memory { get; private set; }

        /// <summary>
        /// The working register model.
        /// </summary>
        public MemoryCell WorkingRegister { get; private set; }

        /// <summary>
        /// Gets or sets the current value of the working register.
        /// </summary>
        public byte W
        {
            get { return WorkingRegister.Value; }
            set { WorkingRegister.Value = value; }
        }

        #endregion

        public void WatchdogReset()
        {
            throw new NotImplementedException();
        }
    }
}
