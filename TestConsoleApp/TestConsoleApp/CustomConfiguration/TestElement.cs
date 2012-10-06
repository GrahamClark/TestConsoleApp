using System.Configuration;

namespace TestConsoleApp.CustomConfiguration
{
    public sealed class TestElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string TestKey
        {
            get { return (string)base["key"]; }
        }

        [ConfigurationProperty("text", IsRequired = true)]
        public string TestText
        {
            get { return (string)base["text"]; }
        }
    }
}
