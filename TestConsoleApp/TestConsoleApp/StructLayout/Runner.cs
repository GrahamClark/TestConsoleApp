using System;
using System.Runtime.InteropServices;

namespace TestConsoleApp.StructLayout
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            S s = new S(400);
            s.Write();
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct S
    {
        [FieldOffset(0)]
        public int first;

        [FieldOffset(0)]
        public int second;

        [FieldOffset(3)]
        public int third;

        public S(int number)
        {
            this.first = this.second = this.third = number;
        }

        public void Write()
        {
            Console.WriteLine(
                "first: {0}{3}second: {1}{3}third: {2}{3}",
                this.first,
                this.second,
                this.third,
                Environment.NewLine);
        }
    }
}
