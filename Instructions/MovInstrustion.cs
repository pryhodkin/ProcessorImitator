namespace ProcessorImitator
{
    class MovInstrustion : IInstruction
    {
        public Register First { get; set; }
        public Register Second { get; set; }
        private Register Third { get; set; }
        public string Instruction { get; set; }

        public int Execute()
        {
            for (int i = 0; i < First.Count; ++i)
                First[i] = Second[i];
            return First[0];
        }

        public MovInstrustion(Register first, Register second, Register third, string instruction)
        {
            First = first;
            Second = second;
            Third = third;
            Instruction = instruction;
        }
    }
}
