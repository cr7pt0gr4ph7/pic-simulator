using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Execution
{
    public partial class Processor
    {
        /// <summary>
        /// Run a single execution step.
        /// </summary>
        public void Step()
        {
            // Prepare the simulation
            PreStep();

            // Execute the simulation step
            DoStep();

            // Do any necessary cleanup work & status updates
            PostStep();
        }

        /// <summary>
        /// Executed before a single step execution to prepare the simulation etc.
        /// </summary>
        private void PreStep()
        {
            // Reset the "changed in current step" flags etc.
            ProgramCounter.PreStep();

            // Read the current values from the serial port
            Communication.PreStep();
        }

        private void DoStep()
        {
            if (Watchdog.Step()) return;
            if (InterruptManager.Step()) return;

            // Read in the opcode at the location the PC is pointing at, and decode it.
            var currentOpcode = ProgramMemory[ProgramCounter.Value];
            Decoder.GetInstructionFromOpcode(currentOpcode);

            //// TODO Remove the debugging output
            //Console.WriteLine("Instruction: {0} ({1})", instruction.GetType().Name, currentOpcode.ToString("X"));

            //// TODO Clean up the instruction initialization logic
            //instruction.Processor = this;
            //instruction.Execute(currentOpcode);
        }

        /// <summary>
        /// Executed after a single step execution to update the simulation status etc.
        /// </summary>
        private void PostStep()
        {
            // Advance the virtual time by the appropriate amount:
            //   - Operations that modify the program counter need 2Tcy, according to the PIC16C84 manual.
            //   - All other operations only need one instruction cycle.

            if (ProgramCounter.ChangedInCurrentStep) {
                Clock.AdvanceBy(2);
            } else {
                Clock.AdvanceBy(1);
            }

            // Advance the program counter, if it has not already been loaded during the execution
            // of the current step; in the latter case the PC is *not* incremented.

            ProgramCounter.PostStep();

            // Write the newest values to the serial port
            Communication.PostStep();
        }
    }
}
