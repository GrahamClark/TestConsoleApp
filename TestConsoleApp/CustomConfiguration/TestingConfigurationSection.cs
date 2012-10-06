using System.Configuration;

namespace TestConsoleApp.CustomConfiguration
{
    public sealed class TestingConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("profiles", IsDefaultCollection = false)]
        public ProfileElementCollection Profiles
        {
            get { return this["profiles"] as ProfileElementCollection; }
        }
    }
}
