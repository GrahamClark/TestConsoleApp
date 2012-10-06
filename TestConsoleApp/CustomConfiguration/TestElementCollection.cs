using System;
using System.Configuration;

namespace TestConsoleApp.CustomConfiguration
{
    [ConfigurationCollection(typeof(TestElement), AddItemName = "test", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public sealed class TestElementCollection : ConfigurationElementCollection
    {
        public TestElement this[int index]
        {
            get { return (TestElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }

                base.BaseAdd(index, value);
            }
        }

        public new string this[string key]
        {
            get
            {
                var element = (TestElement)base.BaseGet(key);
                return element == null ? String.Empty : element.TestText;
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "test"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TestElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TestElement)element).TestKey;
        }
    }
}
