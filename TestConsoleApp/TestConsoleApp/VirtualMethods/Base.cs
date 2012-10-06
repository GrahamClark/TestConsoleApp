namespace TestConsoleApp.VirtualMethods
{
    internal abstract class Base
    {
        internal abstract string Main();

        protected string Do()
        {
            string s = "In Base.Do().\n";
            return Extra(s);
        }

        protected virtual string Extra(string s)
        {
            return s + "In Base.Extra().\n";
        }
    }
}
