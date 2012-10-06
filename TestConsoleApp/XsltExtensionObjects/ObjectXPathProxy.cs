using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Xml;
using System.Reflection;

namespace TestConsoleApp.XsltExtensionObjects
{
    public class ObjectXPathProxy
    {
        /// <summary>
        /// Cannonical message representation, bound to this object proxy
        /// </summary>
        private object binding;

        private string m_name;
        private bool m_activated = false;
        private HybridDictionary m_attributes = null;
        private string[] m_attributeKeys = null;
        private List<ObjectXPathProxy> m_elements = null;
        private Hashtable m_elemDict = null;
        private ObjectXPathProxy m_parent = null;
        private XmlNameTable m_nt;
        private static object[] m_empty = new object[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectXPathProxy"/> class.
        /// </summary>
        /// <param name="bindingObj">The binding obj.</param>
        /// <param name="nt">The nt.</param>
        public ObjectXPathProxy(object bindingObj, XmlNameTable nt)
        {
            binding = bindingObj;
            m_nt = nt;
            m_name = GetAtomicString(binding.GetType().Name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectXPathProxy"/> class.
        /// </summary>
        /// <param name="bindingObj">The binding obj.</param>
        /// <param name="name">The name.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="nt">The nt.</param>
        private ObjectXPathProxy(
            object bindingObj, string name, ObjectXPathProxy parent, XmlNameTable nt)
        {
            binding = bindingObj;
            m_parent = parent;
            m_nt = nt;
            m_name = GetAtomicString(name);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return m_name; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public ObjectXPathProxy Parent
        {
            get { return m_parent; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                if (HasText)
                {
                    return CultureSafeToString(binding);
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has attributes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has attributes; otherwise, <c>false</c>.
        /// </value>
        public bool HasAttributes
        {
            get
            {
                Activate();

                return (m_attributes != null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has children.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has children; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildren
        {
            get
            {
                Activate();
                if (Value != String.Empty)
                {
                    return false;
                }
                return (m_elements != null) || HasText;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has text.
        /// </summary>
        /// <value><c>true</c> if this instance has text; otherwise, <c>false</c>.</value>
        public bool HasText
        {
            get
            {
                Type t = binding.GetType();

                return (t.IsValueType || t == typeof(string));
            }
        }

        /// <summary>
        /// Gets the attribute keys.
        /// </summary>
        /// <value>The attribute keys.</value>
        public IList AttributeKeys
        {
            get
            {
                Activate();

                if (m_attributeKeys != null)
                {
                    return m_attributeKeys;
                }
                else
                {
                    return m_empty;
                }
            }
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetAttributeValue(string name)
        {
            string v = null;

            Activate();

            if (m_attributes != null)
            {
                v = (string)m_attributes[name];
            }

            return (v != null) ? v : string.Empty;
        }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <value>The elements.</value>
        public List<ObjectXPathProxy> Elements
        {
            get
            {
                Activate();

                if (m_elements != null)
                {
                    return m_elements;
                }
                else
                {
                    //return m_empty;
                    return new List<ObjectXPathProxy>(0);
                }
            }
        }

        /// <summary>
        /// Gets the element dictionary.
        /// </summary>
        /// <value>The element dictionary.</value>
        public IDictionary ElementDictionary
        {
            get
            {
                Activate();

                return m_elemDict;
            }
        }

        /// <summary>
        /// Adds the name of the special.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="val">The val.</param>
        public void AddSpecialName(string key, string val)
        {
            Activate();

            if (m_attributes == null)
            {
                m_attributes = new HybridDictionary();
            }

            m_attributes["*" + key] = val;

            m_attributeKeys = new string[m_attributes.Count];
            m_attributes.Keys.CopyTo(m_attributeKeys, 0);
        }

        /// <summary>
        /// Activates this instance.
        /// </summary>
        private void Activate()
        {
            if (m_activated)
            {
                return;
            }

            lock (this)
            {
                if (m_activated)
                {
                    return;
                }

                if (binding is ValueType || binding is string)
                {
                    // no attributes or children
                }
                else if (binding is IDictionary)
                {
                    ActivateDictionary();
                }
                else if (binding is ICollection)
                {
                    ActivateCollection();
                }
                else if (binding is IList<Int32>)
                {
                    ActivateIntCollection();
                }
                else
                {
                    ActivateSimple();
                }

                m_activated = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ActivateIntCollection()
        {
            HybridDictionary attributes = new HybridDictionary();
            foreach (Int32 var in (IList<Int32>)binding)
            {
                attributes.Add(GetAtomicString("Int32"), var.ToString());
            }
            m_attributes = (attributes.Count != 0) ? attributes : null;

            if (m_attributes != null)
            {
                m_attributeKeys = new string[m_attributes.Count];
                m_attributes.Keys.CopyTo(m_attributeKeys, 0);
            }
        }

        /// <summary>
        /// Activates the collection.
        /// </summary>
        private void ActivateCollection()
        {
            List<ObjectXPathProxy> elements = new List<ObjectXPathProxy>();

            foreach (object val in (ICollection)binding)
            {
                if (val == null)
                {
                    continue;
                }

                elements.Add(new ObjectXPathProxy(val, val.GetType().Name, this, m_nt));
            }

            m_elements = (elements.Count != 0) ? elements : null;
        }

        /// <summary>
        /// Activates the dictionary.
        /// </summary>
        private void ActivateDictionary()
        {
            List<ObjectXPathProxy> elements = new List<ObjectXPathProxy>();

            m_elemDict = new Hashtable();

            foreach (DictionaryEntry entry in (IDictionary)binding)
            {
                if (entry.Value == null)
                {
                    continue;
                }

                ObjectXPathProxy item =
                    new ObjectXPathProxy(entry.Value, entry.Value.GetType().Name, this, m_nt);

                elements.Add(item);

                item.AddSpecialName("key", entry.Key.ToString());

                m_elemDict[entry.Key.ToString()] = item;
            }

            m_elements = (elements.Count != 0) ? elements : null;
        }

        /// <summary>
        /// Activates the simple.
        /// </summary>
        private void ActivateSimple()
        {
            HybridDictionary attributes = new HybridDictionary();
            List<ObjectXPathProxy> elements = new List<ObjectXPathProxy>();

            foreach (
                PropertyInfo pi in
                    binding.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // get the value
                object val = pi.GetValue(binding, m_empty);

                if (val == null)
                {
                    continue;
                }

                string str = CultureSafeToString(val);

                elements.Add(new ObjectXPathProxy(str ?? val, pi.Name, this, m_nt));
            }

            m_attributes = (attributes.Count != 0) ? attributes : null;
            m_elements = (elements.Count != 0) ? elements : null;

            if (m_attributes != null)
            {
                m_attributeKeys = new string[m_attributes.Count];
                m_attributes.Keys.CopyTo(m_attributeKeys, 0);
            }
        }

        /// <summary>
        /// Gets the atomic string.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        private string GetAtomicString(string v)
        {
            string s;

            s = m_nt.Get(v);

            if (s == null)
            {
                s = m_nt.Add(v);
            }

            return s;
        }

        /// <summary>
        /// Cultures the safe to string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        private string CultureSafeToString(object obj)
        {
            if (obj is string)
            {
                return (string)obj;
            }

            if (obj is ValueType)
            {
                // handle DateTime
                if (obj is DateTime)
                {
                    string fmt = "yyyy-MM-dd";
                    DateTime dt = (DateTime)obj;

                    if (dt.TimeOfDay.Ticks > 0)
                    {
                        fmt += " HH:mm:ss";
                    }

                    return dt.ToString(fmt);
                }

                // specific handling for floating point types
                if ((obj is decimal) || (obj is float) || (obj is double))
                {
                    return
                        ((IFormattable)obj).ToString(
                            null, CultureInfo.InvariantCulture.NumberFormat);
                }

                if (obj is Enum)
                {
                    return ((Enum)obj).ToString();
                }

                if (obj is Guid)
                {
                    return ((Guid)obj).ToString("D");
                }

                // generic handling for all other value types
                return obj.ToString();
            }

            // objects return null
            return null;
        }
    }
}
