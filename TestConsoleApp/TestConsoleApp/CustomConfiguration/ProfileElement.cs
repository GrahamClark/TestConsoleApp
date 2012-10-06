using System.Configuration;

namespace TestConsoleApp.CustomConfiguration
{
    public sealed class ProfileElement : ConfigurationElement
    {
        [ConfigurationProperty("profileName", IsRequired = true)]
        public string ProfileName
        {
            get { return (string)base["profileName"]; }
        }

        [ConfigurationProperty("tests", IsRequired = false)]
        public TestElementCollection Tests
        {
            get { return this["tests"] as TestElementCollection; }
        }
    }
}
