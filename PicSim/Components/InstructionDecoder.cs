using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Instructions;
using PicSim.Utils;
using Opcode = System.UInt16;
using Instruction = System.Object;

namespace PicSim.Components
{
    public class InstructionDecoder
    {
        public InstructionOps ops { get; set; }
        private static readonly BitVector32.Section ms_highestTwoBits = SectionUtils.ForRange(12, 13);
        private static readonly int ms_nextBit = (1 << 11);
        private static readonly BitVector32.Section ms_nextTwoBits = SectionUtils.ForRange(10, 11);
        private static readonly BitVector32.Section ms_nextThreeBits = SectionUtils.ForRange(9, 11);
        private static readonly BitVector32.Section ms_nextFourBits = SectionUtils.ForRange(8, 11);

        private static readonly int ms_highestBitOfLastNibble = (1 << 7);
        private static readonly BitVector32.Section ms_secondLastNibble = SectionUtils.ForRange(4, 7);
        private static readonly BitVector32.Section ms_lastNibble = SectionUtils.ForRange(0, 3);
        private static readonly BitVector32.Section ms_lastByte = SectionUtils.ForRange(0, 7);

        /// <summary>
        /// Returns the instruction that corresponds to the specified opcode.
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public Instruction GetInstructionFromOpcode(Opcode opcode)
        {
            ops.Opcode = opcode;

            BitVector32 vector = new BitVector32(opcode);

            switch (vector[ms_highestTwoBits]) {
                case 0x00: /* 00b */
                    return GetByteOrientedInstruction(vector);

                case 0x01: /* 01b */
                    return GetBitOrientedInstruction(vector);

                case 0x02: /* 10b */
                    if (!vector[ms_nextBit]) {
                        ops.CALL(); return null;
                    } else {
                        ops.GOTO(); return null;
                    }

                case 0x03: /* 11b */
                    return GetLiteralInstruction(vector);
            }

            throw new InvalidOperationException("Should not get here because of exhaustive switch");
        }

        private Instruction GetByteOrientedInstruction(BitVector32 vector)
        {
            switch (vector[ms_nextFourBits]) {
                case 0x00: /* 0000 */ return GetOtherOperation(vector);
                case 0x01: /* 0001 */ ops.CLRFOrW(); return null;
                case 0x02: /* 0010 */ ops.SUBWF(); return null;
                case 0x03: /* 0011 */ ops.DECF(); return null;
                case 0x04: /* 0100 */ ops.IORWF(); return null;
                case 0x05: /* 0101 */ ops.ANDWF(); return null;
                case 0x06: /* 0110 */ ops.XORWF(); return null;
                case 0x07: /* 0111 */ ops.ADDWF(); return null;
                case 0x08: /* 1000 */ ops.MOVF(); return null;
                case 0x09: /* 1001 */ ops.COMF(); return null;
                case 0x0A: /* 1010 */ ops.INCF(); return null;
                case 0x0B: /* 1011 */ ops.DECFSZ(); return null;
                case 0x0C: /* 1100 */ ops.RRF(); return null;
                case 0x0D: /* 1101 */ ops.RLF(); return null;
                case 0x0E: /* 1110 */ ops.SWAPF(); return null;
                case 0x0F: /* 1111 */ ops.INCFSZ(); return null;
            }

            throw new InvalidOperationException("Should not get here because of exhaustive switch");
        }

        private Instruction GetOtherOperation(BitVector32 vector)
        {
            if (vector[ms_highestBitOfLastNibble]) {
                ops.MOVWF(); return null;
            }

            switch (vector[ms_lastByte]) {
                case 0x00: /* 0xx0 0000 */
                case 0x20: /*           */
                case 0x40: /*           */
                case 0x60: /*           */ ops.NOP(); return null;
                case 0x08: /* 0000 1000 */ ops.RETURN(); return null;
                case 0x09: /* 0000 1001 */ ops.RETFIE(); return null;
                case 0x63: /* 0110 0011 */ ops.SLEEP(); return null;
                case 0x64: /* 0110 0100 */ ops.CLRWDT(); return null;
            }

            throw new InvalidOperationException("Unknown opcode: " + vector.ToString());
        }

        private Instruction GetBitOrientedInstruction(BitVector32 vector)
        {
            switch (vector[ms_nextTwoBits]) {
                case 0x00: /* 00 */ ops.BCF(); return null;
                case 0x01: /* 01 */ ops.BSF(); return null;
                case 0x02: /* 10 */ ops.BTFSC(); return null;
                case 0x03: /* 11 */ ops.BTFSS(); return null;
            }

            throw new InvalidOperationException("Should not get here because of exhaustive switch");
        }


        private Instruction GetLiteralInstruction(BitVector32 vector)
        {
            switch (vector[ms_nextFourBits]) {
                case 0x00: /* 00xx */
                case 0x01: /*      */
                case 0x02: /*      */
                case 0x03: /*      */ ops.MOVLW(); return null;
                case 0x04: /* 01xx */
                case 0x05: /*      */
                case 0x06: /*      */
                case 0x07: /*      */ ops.RETLW(); return null;
                case 0x08: /* 1000 */ ops.IORLW(); return null;
                case 0x09: /* 1001 */ ops.ANDLW(); return null;
                case 0x0A: /* 1010 */ ops.XORLW(); return null;
                case 0x0B: /* 1011 */ throw new NotImplementedException("Invalid opcode");
                case 0x0C: /* 110x */
                case 0x0D: /*      */ ops.SUBLW(); return null;
                case 0x0E: /* 111x */
                case 0x0F: /*      */ ops.ADDLW(); return null;
            }

            throw new InvalidOperationException("Should not get here because of exhaustive switch");
        }
    }
}
