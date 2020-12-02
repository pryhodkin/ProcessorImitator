using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessorImitator
{
    interface IInstruction
    {
        string Instruction { get; set; }
        int Execute();
    }
}
