namespace TestConsoleApp.Pointers
{
    internal class Runner : IRunner
    {
        public void RunProgram()
        {
            double[] arr = new double[10];
            var p0 = (ArrayPtr<double>)arr;
            var p5 = p0 + 5;
            p5[0] = 123.4; // sets arr[5] to 123.4
            System.Console.WriteLine(arr[5]);

            var p7 = p0 + 7;
            int diff = p7 - p5; // 2
            System.Console.WriteLine(diff);
        }
    }
}
