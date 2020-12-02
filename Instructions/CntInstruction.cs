using System.Collections.ObjectModel;
using System.Linq;

namespace ProcessorImitator
{
    internal class CntInstruction : IInstruction
    {
        private Register First { get; set; }
        private Register Second { get; set; }
        private Register Third { get; set; }
        public string Instruction { get; set; }

        public CntInstruction(Register first, Register second, Register third, string instruction)
        {
            First = first;
            Second = second;
            Third = third;
            Instruction = instruction;
        }

        public int Execute()
        {
            char c;
            if (Second[2] == 0)
                c = '0';
            else
                c = '1';
            
            Third[2] = (sbyte)(First.ToString().Count(item => item == c));
            Third[0] = 0;
            Third[1] = 0;
            return Third[0];
        }
    }
}