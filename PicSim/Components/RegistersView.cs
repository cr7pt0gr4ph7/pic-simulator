﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Components.Registers;
using PicSim.Components.Communication;

namespace PicSim.Components
{
    public class RegistersView
    {
        static IRegister CreateTemp()
        {
            return new MemoryCell();
        }

        public RegistersView(ProgramCounter programCounter, CommunicationManager communication, Timer timer)
        {
            // TODO Implement the other registers

            this.TMR0 = timer.TMR0Register;
            this.Option = new OptionRegister();

            this.PCL = programCounter.PCL_Register;
            this.Status = new StatusRegister();
            this.FSR = new MemoryCell();

            this.PORTA = communication.PORTA;
            this.TRISA = communication.TRISA;
            this.PORTB = communication.PORTB;
            this.TRISB = communication.TRISB;

            this.EEDATA = CreateTemp();
            this.EECON1 = CreateTemp();
            this.EEADR = CreateTemp();
            this.EECON2 = CreateTemp();

            this.PCLATH = programCounter.PCLATH_Register;
            this.INTCON = new IntconRegister();
        }

        public IRegister TMR0 { get; private set; }
        public OptionRegister Option { get; private set; }

        public IRegister PCL { get; private set; }
        public StatusRegister Status { get; private set; }
        public MemoryCell FSR { get; private set; }

        public IRegister PORTA { get; private set; }
        public IRegister TRISA { get; private set; }
        public IRegister PORTB { get; private set; }
        public IRegister TRISB { get; private set; }

        public IRegister EEDATA { get; private set; }
        public IRegister EECON1 { get; private set; }
        public IRegister EEADR { get; private set; }
        public IRegister EECON2 { get; private set; }

        public IRegister PCLATH { get; private set; }
        public IntconRegister INTCON { get; private set; }
    }
}
