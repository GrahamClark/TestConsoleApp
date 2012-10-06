namespace TestConsoleApp.VirtualMethods
{
    class AnotherChild : Base
    {
        internal override string Main()
        {
            System.Console.WriteLine("In AnotherChild.Main()");
            return base.Do();
        }
    }
}
