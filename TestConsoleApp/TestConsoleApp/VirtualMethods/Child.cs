namespace TestConsoleApp.VirtualMethods
{
    class Child : Base
    {
        internal override string Main()
        {
            return base.Do();
        }

        protected override string Extra(string s)
        {
            return s + "In Child.Extra().\n";
        }
    }
}
