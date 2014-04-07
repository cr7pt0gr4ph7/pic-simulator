using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Execution;

namespace PicSim.Instructions
{
    public static class Operations
    {
        private const byte FULL_BYTE = 0xFF;
        private const byte HIGHER_NIBBLE = 0xF0;
        private const byte LOWER_NIBBLE = 0x0F;

        private static OperationResult add_impl(int x, int y)
        {
            int result = (x & FULL_BYTE) + (y & FULL_BYTE);
            bool digitCarry = ((x & LOWER_NIBBLE) + (y & LOWER_NIBBLE)) > LOWER_NIBBLE;
            bool carry = ((0x100 & result) == 0x100);
            return new OperationResult((byte)(result & FULL_BYTE), carry, digitCarry);
        }

        public static OperationResult Add(byte x, byte y)
        {
            return add_impl(x, y);
        }

        public static OperationResult Sub(byte x, byte y)
        {
            return add_impl(x, -y);
        }

        public static void HandleResult(Processor processor, byte result)
        {
            HandleResult(processor, result, false, /*not used*/0);
        }

        public static void HandleResult(Processor processor, byte result, bool destination, byte address)
        {
            processor.CheckZero(result);

            if (destination) {
                // If 'd' is 1 the result is stored back in register 'f':
                processor.Memory[address] = result;
            } else {
                // If 'd' is 0 the result is stored back in the W register:
                processor.W = result;
            }
        }

        public static void HandleAddOrSub(Processor processor, OperationResult result)
        {
            HandleAddOrSub(processor, result, false, /*not used*/0);
        }

        public static void HandleAddOrSub(Processor processor, OperationResult result, bool destination, byte address)
        {
            processor.SetStatus(result);

            if (destination) {
                // If 'd' is 1 the result is stored back in register 'f':
                processor.Memory[address] = result.Value;
            } else {
                // If 'd' is 0 the result is stored back in the W register:
                processor.W = result.Value;
            }
        }
    }
}