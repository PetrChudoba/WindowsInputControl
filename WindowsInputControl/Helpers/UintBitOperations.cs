using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsInputControl.Helpers
{
    /// <summary>
    /// Class UintBitOperations is extensions class for bit operation on uint values.
    /// </summary>
    public static class UintBitOperations
    {
        /// <summary>
        /// Determines whether a bit is set on the specified position.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="position">The position.</param>
        /// <returns><c>true</c> if [is bit set] [the specified position]; otherwise, <c>false</c>.</returns>
        public static bool IsBitSet(this uint number, int position)
        {
            return (number & (1 << position)) != 0;
        }

        /// <summary>
        /// Gets the binary representation.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>System.String.</returns>
        public static string GetBinaryRepresentation(this uint number)
        {
            return Convert.ToString(number, 2);
        }


        public static string GetBinaryRepresentation(this uint number, int requiredLength)
        {
            string binaryStr = Convert.ToString(number, 2);

            //good length
            if (binaryStr.Length == requiredLength) return binaryStr;


            return binaryStr.Length < requiredLength
                ? binaryStr.PadLeft(requiredLength, '0')
                : binaryStr.Substring(binaryStr.Length - requiredLength);
        }


        /// <summary>
        /// Sets the bit on the specified position to 1.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="pos">The position.</param>
        /// <returns>System.UInt32.</returns>
        public static uint SetBit(this uint number, int pos)
        {
            uint shiftedBit = (uint) 1 << pos;

            return number | shiftedBit;
        }

        /// <summary>
        /// Sets the bit on the specified position to 0.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="pos">The position.</param>
        /// <returns>System.UInt32.</returns>
        public static uint UnsetBit(this uint number, int pos)
        {
            uint shiftedZero = (uint) ~(1 << pos);

            return number & shiftedZero;
        }


        /// <summary>
        /// Changes the bit on the specified position.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="requiredValue">if set to <c>true</c> sets the bit to 1 otherwise sets the bit to 0.</param>
        /// <param name="position">The position.</param>
        /// <returns>System.UInt32.</returns>
        public static uint ChangeBit(this uint number, bool requiredValue, int position)
        {
            return requiredValue ? SetBit(number, position) : UnsetBit(number, position);
        }
    }
}