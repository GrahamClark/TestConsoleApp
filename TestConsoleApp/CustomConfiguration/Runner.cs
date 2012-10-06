using System;
using System.Configuration;

namespace TestConsoleApp.CustomConfiguration
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var testConfig = ConfigurationManager.GetSection("testConfig") as TestingConfigurationSection;
            Console.WriteLine(testConfig.Profiles["one"].Tests["second"]);

            Console.WriteLine("Non-existent profile:");
            Console.WriteLine(testConfig.Profiles["sdf"]);

            Console.WriteLine("\nNon-existent test:");
            Console.WriteLine(testConfig.Profiles["one"].Tests["fsd"]);

            Console.WriteLine("\nProfile with no tests:");
            Console.WriteLine(testConfig.Profiles["two"].Tests["one"]);

            Console.WriteLine("\nCount of Words from first profile:");
            Console.WriteLine(testConfig.Profiles["one"].Tests.Count);
        }
    }
}
