using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProcessorImitator
{
    [AddINotifyPropertyChangedInterface]
    class Register : INotifyPropertyChanged
    {

        #region Public properties

        /// <summary>
        /// Register name, temprary register has no name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Bytes counter.
        /// </summary>
        public int Count => bits.Length;

        /// <summary>
        /// String representation of bits.
        /// </summary>
        public string Bytes { get; set; }

        /// <summary>
        /// An 8-bit group with specified index. 
        /// </summary>
        /// <param name="index">Index of bit group.</param>
        public sbyte this[int index]
        {
            get => bits[index];
            set
            {
                bits[index] = value;
                Bytes = ToString();
            }
        }

        #endregion

        #region Data container

        /// <summary>
        /// Bits.
        /// </summary>
        private sbyte[] bits { get; set; } = new sbyte[3] { 0, 0, 0 };

        #endregion

        #region Static properties

        /// <summary>
        /// All existing registers.
        /// </summary>
        private static List<Register> Registers { get; set; } = new List<Register>();

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion

        #region Object overrides

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var @sbyte in bits)
            {
                int n = Convert.ToString(@sbyte, 2).Count();
                var bitString = "00000000" + Convert.ToString(@sbyte, 2);
                bitString = bitString.Substring(n);
                result.Append(bitString);
            }

            return result.ToString();
        }

        #endregion

        public static Register GetRegister(string name)
        {
            try
            {
                return Registers.Single(r => r.Name == name);
            }
            catch { return null; }
        }

        #region Constructor

        /// <summary>
        /// Initializes register with specified content and name.
        /// </summary>
        /// <param name="n">Content.</param>
        /// <param name="name">Name.</param>
        public Register(int n, string name)
        {
            Name = name;
            bits = To24BitsArray(n);
            if (Registers.FindIndex(c => c.Name == name) == -1)
                Registers.Add(this);
            Bytes = ToString();
        }

        #endregion

        #region helper methods

        /// <summary>
        /// Converts a number to 24-bit (sbyte array of 3).
        /// </summary>
        /// <param name="n">The number to convert.</param>
        /// <returns></returns>
        private static sbyte[] To24BitsArray(int n)
        {
            //Cutting all extra bits.
            n %= (int)Math.Pow(2, 23);

            //Converting number...
            sbyte[] intBytes = BitConverter.GetBytes(n).Select(b => (sbyte)b).ToArray();

            //Reverse if it is necessary.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);

            //Skip first zero byte.
            return intBytes.Skip(1).ToArray();
        }

        #endregion
    }
}
