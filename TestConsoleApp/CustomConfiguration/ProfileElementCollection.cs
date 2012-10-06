using System.Configuration;

namespace TestConsoleApp.CustomConfiguration
{
    [ConfigurationCollection(typeof(ProfileElement), AddItemName = "profile", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public sealed class ProfileElementCollection : ConfigurationElementCollection
    {
        public ProfileElement this[int index]
        {
            get { return (ProfileElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }

                base.BaseAdd(index, value);
            }
        }

        public new ProfileElement this[string key]
        {
            get { return (ProfileElement)base.BaseGet(key); }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "profile"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProfileElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProfileElement)element).ProfileName;
        }
    }
}
