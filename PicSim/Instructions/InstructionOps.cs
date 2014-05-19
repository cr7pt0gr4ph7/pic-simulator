using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Execution;
using PicSim.Utils;
using System.Collections.Specialized;

namespace PicSim.Instructions
{
    public class InstructionOps
    {
        public Processor Processor { get; set; }

        public ByteValue W
        {
            get { return Processor.W; }
            set { Processor.W = value; }
        }

        #region Opcode helpers

        public ushort Opcode { get; set; }

        private static readonly BitVector32.Section ms_immediate_section = SectionUtils.ForRange(0, 7);
        private static readonly BitVector32.Section ms_address_section = SectionUtils.ForRange(0, 10);
        private static readonly BitVector32.Section ms_register_section = SectionUtils.ForRange(0, 6);
        private static readonly BitVector32.Section ms_bit_section = SectionUtils.ForRange(7, 9);
        private static readonly int ms_destination_mask = SectionUtils.MaskForRange(7, 7);

        private BitVector32 vector
        {
            get { return new BitVector32(Opcode); }
            set { Opcode = (byte)value.Data; }
        }

        private bool destination
        {
            get { return vector[ms_destination_mask]; }
        }

        /// <summary>
        /// Call and goto instructions only:
        /// The 11-bit immediate address encoded in the opcode.
        /// </summary>
        private ushort immediate_address
        {
            get { return (ushort)vector[ms_address_section]; }
        }

        /// <summary>
        /// Byte- and bit-oriented instructions:
        /// The 7-bit register address encoded in the opcode.
        /// </summary>
        private ByteValue register_address
        {
            get { return (byte)vector[ms_register_section]; }
        }

        /// <summary>
        /// Bit-oriented instructions only:
        /// The 3-bit bit address encoded in the opcode.
        /// </summary>
        private byte bit_no
        {
            get { return (byte)vector[ms_bit_section]; }
        }

        /// <summary>
        /// Literal and control instructions only:
        /// The 8-bit immediate value 'k' encoded in the opcode.
        /// </summary>
        private ByteValue immediate_value
        {
            get { return (byte)vector[ms_immediate_section]; }
        }

        #endregion

        #region Microcode operations (Common)

        private OperationResult m_temporaryResult = new OperationResult();
        private byte m_temporary;

        /// <summary>
        /// Set <paramref name="value"/> as the value of the temporary register.
        /// </summary>
        /// <param name="value"></param>
        private void set_temporary(ByteValue value)
        {
            m_temporary = value;
            m_temporaryResult = new OperationResult(value, false, false);
        }

        private void set_temporary(OperationResult result)
        {
            m_temporaryResult = result;
            m_temporary = result.Value;
        }

        private void write_destination()
        {
            if (destination) {
                write_f();
            } else {
                write_w();
            }
        }

        /// <summary>
        /// Byte- and bit-oriented instructions only: Read the register 'f'
        /// specified in the opcode into the temporary input register.
        /// </summary>
        private void read_f()
        {
            m_temporary = Processor.Memory[register_address];
        }

        private void write_f()
        {
            Processor.Memory[register_address] = m_temporary;
        }

        private void read_immediate()
        {
            set_temporary(immediate_value);
        }

        private void write_status()
        {
            Processor.SetStatus(m_temporaryResult);
        }

        private void write_zero_flag()
        {
            Processor.CheckZero(m_temporary);
        }

        private void read_w()
        {
            set_temporary(Processor.W);
        }

        private void write_w()
        {
            Processor.W = m_temporary;
        }

        #endregion

        #region Microcode operations (specific)

        //
        // Binary arithmetic operations
        //

        private void add()
        {
            var res = Operations.Add(m_temporary, W);
            set_temporary(res);
        }

        private void sub()
        {
            var res = Operations.Sub(m_temporary, W);
            set_temporary(res);
        }

        private void and()
        {
            set_temporary(W & m_temporary);
        }

        private void inclusive_or()
        {
            set_temporary(W | m_temporary);
        }

        private void xor()
        {
            set_temporary(W ^ m_temporary);
        }

        //
        // Unary arithmetic operations
        //

        private void complement()
        {
            set_temporary((byte)(m_temporary ^ 0xFF));
        }

        private void increment()
        {
            // NOTE: Use unchecked arithmetic to enable silent overflow
            set_temporary((byte)unchecked(m_temporary + 1));
        }

        private void decrement()
        {
            set_temporary((byte)unchecked(m_temporary - 1));
        }

        private void swap_nibbles()
        {
            byte oldUpperHalf = (byte)(m_temporary >> 4);
            byte oldLowerHalf = (byte)((m_temporary << 4) & 0xFF);
            set_temporary((byte)(oldUpperHalf | oldLowerHalf));
        }

        //
        // Status helpers
        //

        private bool is_zero()
        {
            return (m_temporary == 0);
        }

        //
        // Bit operations
        //

        private bool get_bit()
        {
            return m_temporary.GetBit(bit_no);
        }

        private void set_bit()
        {
            set_temporary(m_temporary.SetBit(bit_no));
        }

        private void clear_bit()
        {
            set_temporary(m_temporary.ClearBit(bit_no));
        }

        //
        // Control operations
        //

        private void skip_next_instruction()
        {
            // TODO This does not cover wraparound!
            Processor.ProgramCounter.LoadFrom13Bits((byte)(Processor.ProgramCounter.Value + 2));
        }

        private void load_pc_11bits(ushort address)
        {
            Processor.ProgramCounter.LoadFrom11Bits(address);
        }

        private void push_pc()
        {
            Processor.Stack.Push(Processor.ProgramCounter.NextValue);
        }

        private void pop_pc()
        {
            Processor.ProgramCounter.LoadFrom11Bits(Processor.Stack.Pop());
        }

        #endregion

        public void ADDLW()
        {
            read_immediate();
            add();
            write_status();
            write_w();
        }

        public void ADDWF()
        {
            read_f();
            add();
            write_status();
            write_destination();
        }

        public void ANDLW()
        {
            read_immediate();
            and();
            write_zero_flag();
            write_w();
        }

        public void ANDWF()
        {
            read_f();
            and();
            write_zero_flag();
            write_destination();
        }

        public void BCF()
        {
            read_f();
            clear_bit();
            write_f();
        }

        public void BSF()
        {
            read_f();
            set_bit();
            write_f();
        }

        public void BTFSC()
        {
            read_f();
            if (!get_bit()) {
                skip_next_instruction();
            }
        }

        public void BTFSS()
        {
            read_f();
            if (get_bit()) {
                skip_next_instruction();
            }
        }

        public void CALL()
        {
            push_pc();
            load_pc_11bits(immediate_address);
        }

        public void CLRFOrW()
        {
            if (destination) {
                read_f();
            }
            set_temporary(0);
            write_zero_flag();
            write_destination();
        }

        public void CLRWDT()
        {
            // Clear WDT counter & prescaler
            Processor.Watchdog.Clear();

            // Clear !TO and !PD
            Processor.Registers.Status.NotTimeOut = true;
            Processor.Registers.Status.NotPowerDown = true;
        }

        public void COMF()
        {
            read_f();
            complement();
            write_zero_flag();
            write_destination();
        }

        public void DECF()
        {
            read_f();
            decrement();
            write_zero_flag();
            write_destination();
        }

        public void DECFSZ()
        {
            read_f();
            decrement();
            write_destination();

            if (is_zero()) {
                skip_next_instruction();
            }
        }

        public void GOTO()
        {
            load_pc_11bits(immediate_address);
        }

        public void INCF()
        {
            read_f();
            increment();
            write_zero_flag();
            write_destination();
        }

        public void INCFSZ()
        {
            read_f();
            increment();
            write_destination();

            if (is_zero()) {
                skip_next_instruction();
            }
        }

        public void IORLW()
        {
            read_immediate();
            inclusive_or();
            write_zero_flag();
            write_destination();
        }

        public void IORWF()
        {
            read_f();
            inclusive_or();
            write_zero_flag();
            write_destination();
        }

        public void MOVF()
        {
            read_f();
            write_zero_flag();
            write_destination();
        }

        public void MOVLW()
        {
            read_immediate();
            write_w();
        }

        public void MOVWF()
        {
            read_w();
            write_f();
        }

        public void NOP()
        {
            // Do nothing
        }

        public void RETFIE()
        {
            // Pop return address from stack and load it into the PC
            pop_pc();

            // Reenable the GIE bit
            Processor.Registers.INTCON.GIE = true;
        }

        public void RETLW()
        {
            read_immediate();
            write_w();
            pop_pc();
        }

        public void RETURN()
        {
            pop_pc();
        }

        public void RLF()
        {
            read_f();
            var result = (m_temporary << 1) & (Processor.Registers.Status.Carry ? 1 : 0);
            Processor.Registers.Status.Carry = (result & 0x100) != 0;
            set_temporary((byte)(result & 0xFF));
            write_f();
        }

        public void RRF()
        {
            read_f();
            int input = (Processor.Registers.Status.Carry ? (1 << 8) : 0) | m_temporary;
            Processor.Registers.Status.Carry = (m_temporary & 0x01) != 0;
            int result = (input >> 1);
            set_temporary((byte)(result & 0xFF));
            write_f();
        }

        public void SLEEP()
        {
            throw new NotImplementedException("SLEEP");
        }

        public void SUBLW()
        {
            read_immediate();
            sub();
            write_status();
            write_w();
        }

        public void SUBWF()
        {
            read_f();
            sub();
            write_status();
            write_destination();
        }

        public void SWAPF()
        {
            read_f();
            swap_nibbles();
            write_destination();
        }

        public void XORLW()
        {
            read_immediate();
            xor();
            write_zero_flag();
            write_w();
        }

        public void XORWF()
        {
            read_f();
            xor();
            write_zero_flag();
            write_w();
        }
    }
}