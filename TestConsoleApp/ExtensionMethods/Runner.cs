namespace TestConsoleApp.ExtensionMethods
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            SubItem s = new SubItem()
            {
                Count = 10,
                Details = "details",
                Name = "Bob"
            };
        }
    }
}
