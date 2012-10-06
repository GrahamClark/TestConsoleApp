using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace TestConsoleApp.XsltExtensionObjects
{
    public class ObjectXPathNavigator : XPathNavigator
    {
        private ObjectXPathProxy m_docElem = null;
        private ObjectXPathProxy m_currentElem = null;
        private XPathNodeType m_nodeType = XPathNodeType.Root;
        private IList m_values = null;
        private int m_valueIndex = -1;
        private XmlNameTable m_nt = new NameTable();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectXPathNavigator"/> class.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public ObjectXPathNavigator(object obj)
        {
            m_docElem = new ObjectXPathProxy(obj, m_nt);

            m_docElem.AddSpecialName("type", obj.GetType().FullName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectXPathNavigator"/> class.
        /// </summary>
        private ObjectXPathNavigator()
        { }

        /// <summary>
        /// When overridden in a derived class, gets the base URI for the current node.
        /// </summary>
        /// <value></value>
        /// <returns>The location from which the node was loaded, or <see cref="F:System.String.Empty"></see> if there is no value.</returns>
        public override string BaseURI
        {
            // we don't expose a namespace right now
            get { return string.Empty; }
        }

        /// <summary>
        /// Gets a value indicating whether the current node has any attributes.
        /// </summary>
        /// <value></value>
        /// <returns>Returns true if the current node has attributes; returns false if the current node has no attributes, or if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is not positioned on an element node.</returns>
        public override bool HasAttributes
        {
            get
            {
                switch (m_nodeType)
                {
                    case XPathNodeType.Element:
                        {
                            // does the element have attributes?
                            return m_currentElem.HasAttributes;
                        }

                    default:
                        {
                            // nothing has attributes except elements
                            return false;
                        }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current node has any child nodes.
        /// </summary>
        /// <value></value>
        /// <returns>Returns true if the current node has any child nodes; otherwise, false.</returns>
        public override bool HasChildren
        {
            get
            {
                switch (m_nodeType)
                {
                    case XPathNodeType.Element:
                        {
                            // does the element have children?
                            return m_currentElem.HasChildren;
                        }

                    case XPathNodeType.Root:
                        {
                            // the root always has at least one child
                            // (for the object the navigator was built from)
                            return true;
                        }

                    default:
                        {
                            // nothing else has children
                            return false;
                        }
                }
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current node is an empty element without an end element tag.
        /// </summary>
        /// <value></value>
        /// <returns>Returns true if the current node is an empty element; otherwise, false.</returns>
        public override bool IsEmptyElement
        {
            get
            {
                // we are empty if we don't have children
                return !HasChildren;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="P:System.Xml.XPath.XPathNavigator.Name"></see> of the current node without any namespace prefix.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.String"></see> that contains the local name of the current node, or <see cref="F:System.String.Empty"></see> if the current node does not have a name (for example, text or comment nodes).</returns>
        public override string LocalName
        {
            // we don't use namespaces, so our Name and LocalName are the same
            get { return Name; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the qualified name of the current node.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.String"></see> that contains the qualified <see cref="P:System.Xml.XPath.XPathNavigator.Name"></see> of the current node, or <see cref="F:System.String.Empty"></see> if the current node does not have a name (for example, text or comment nodes).</returns>
        public override string Name
        {
            get
            {
                switch (m_nodeType)
                {
                    case XPathNodeType.Element:
                        {
                            return m_currentElem.Name;
                        }

                    case XPathNodeType.Attribute:
                        {
                            if (m_valueIndex >= 0 && m_valueIndex < m_values.Count)
                            {
                                string s = (string)m_values[m_valueIndex];

                                if (s[0] == '*')
                                {
                                    s = s.Substring(1);
                                }

                                return s;
                            }

                            break;
                        }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the namespace URI of the current node.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.String"></see> that contains the namespace URI of the current node, or <see cref="F:System.String.Empty"></see> if the current node has no namespace URI.</returns>
        public override string NamespaceURI
        {
            get
            {
                switch (m_nodeType)
                {
                    case XPathNodeType.Attribute:
                        {
                            if (m_valueIndex >= 0 && m_valueIndex < m_values.Count)
                            {
                                string s = (string)m_values[m_valueIndex];

                                if (s[0] == '*')
                                {
                                    return "urn:ObjectXPathNavigator";
                                }
                            }

                            break;
                        }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNodeType"></see> of the current node.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Xml.XPath.XPathNodeType"></see> values representing the current node.</returns>
        public override XPathNodeType NodeType
        {
            get { return m_nodeType; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Xml.XmlNameTable"></see> of the <see cref="T:System.Xml.XPath.XPathNavigator"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref="T:System.Xml.XmlNameTable"></see> object enabling you to get the atomized version of a <see cref="T:System.String"></see> within the XML document.</returns>
        public override XmlNameTable NameTable
        {
            get { return m_nt; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the namespace prefix associated with the current node.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.String"></see> that contains the namespace prefix associated with the current node.</returns>
        public override string Prefix
        {
            get
            {
                switch (m_nodeType)
                {
                    case XPathNodeType.Attribute:
                        {
                            if (m_valueIndex >= 0 && m_valueIndex < m_values.Count)
                            {
                                string s = (string)m_values[m_valueIndex];

                                if (s[0] == '*')
                                {
                                    return "oxp";
                                }
                            }

                            break;
                        }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the string value of the item.
        /// </summary>
        /// <value></value>
        /// <returns>The string value of the item.</returns>
        public override string Value
        {
            get
            {
                switch (m_nodeType)
                {
                    case XPathNodeType.Element:
                        {
                            return m_currentElem.Value;
                        }

                    case XPathNodeType.Attribute:
                        {
                            if (m_valueIndex >= 0 && m_valueIndex < m_values.Count)
                            {
                                return m_currentElem.GetAttributeValue((string)m_values[m_valueIndex]);
                            }
                            break;
                        }

                    case XPathNodeType.Text:
                        {
                            goto case XPathNodeType.Element;
                        }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the xml:lang scope for the current node.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.String"></see> that contains the value of the xml:lang scope, or <see cref="F:System.String.Empty"></see> if the current node has no xml:lang scope value to return.</returns>
        public override string XmlLang
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Xml.XPath.XPathNavigator"></see> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator"></see>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Xml.XPath.XPathNavigator"></see> positioned at the same node as this <see cref="T:System.Xml.XPath.XPathNavigator"></see>.
        /// </returns>
        public override XPathNavigator Clone()
        {
            ObjectXPathNavigator newNav = new ObjectXPathNavigator();

            newNav.m_docElem = m_docElem;
            newNav.m_currentElem = m_currentElem;
            newNav.m_nodeType = m_nodeType;
            newNav.m_values = m_values;
            newNav.m_valueIndex = m_valueIndex;
            newNav.m_nt = m_nt;

            return newNav;
        }

        /// <summary>
        /// Gets the value of the attribute with the specified local name and namespace URI.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param>
        /// <param name="namespaceURI">The namespace URI of the attribute.</param>
        /// <returns>
        /// A <see cref="T:System.String"></see> that contains the value of the specified attribute; <see cref="F:System.String.Empty"></see> if a matching attribute is not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is not positioned on an element node.
        /// </returns>
        public override string GetAttribute(string localName, string namespaceURI)
        {
            if (m_nodeType == XPathNodeType.Element)
            {
                if (namespaceURI.Length == 0)
                {
                    return m_currentElem.GetAttributeValue(localName);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <param name="localName">Name of the local.</param>
        /// <returns></returns>
        public override string GetNamespace(string localName)
        {
            return string.Empty;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Xml.XPath.XPathNavigator"></see> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator"></see>.
        /// </summary>
        /// <param name="nav">The <see cref="T:System.Xml.XPath.XPathNavigator"></see> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator"></see>.</param>
        /// <returns>
        /// Returns true if the specified <see cref="T:System.Xml.XPath.XPathNavigator"></see> is a descendant of the current <see cref="T:System.Xml.XPath.XPathNavigator"></see>; otherwise, false.
        /// </returns>
        public override bool IsDescendant(XPathNavigator nav)
        {
            if (nav is ObjectXPathNavigator)
            {
                ObjectXPathNavigator otherNav = (ObjectXPathNavigator)nav;

                // if they're in different graphs, they're not the same
                if (this.m_docElem != otherNav.m_docElem)
                {
                    return false;
                }

                if (m_nodeType == XPathNodeType.Root && otherNav.m_nodeType != XPathNodeType.Root)
                {
                    // its on my root element - its still a descendant
                    return true;
                }

                // if I'm not on an element, it can't be my descendant
                // (attributes and text don't have descendants)
                if (m_nodeType != XPathNodeType.Element)
                {
                    return false;
                }

                if (this.m_currentElem == otherNav.m_currentElem)
                {
                    // if its on my attribute or content - its still a descendant
                    return
                        (m_nodeType == XPathNodeType.Element &&
                         otherNav.m_nodeType != XPathNodeType.Element);
                }

                // ok, we need to hunt...
                for (ObjectXPathProxy parent = otherNav.m_currentElem.Parent;
                    parent != null;
                    parent = parent.Parent)
                {
                    if (parent == this.m_currentElem)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// When overridden in a derived class, determines whether the current <see cref="T:System.Xml.XPath.XPathNavigator"></see> is at the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator"></see>.
        /// </summary>
        /// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator"></see> to compare to this <see cref="T:System.Xml.XPath.XPathNavigator"></see>.</param>
        /// <returns>
        /// Returns true if the two <see cref="T:System.Xml.XPath.XPathNavigator"></see> objects have the same position; otherwise, false.
        /// </returns>
        public override bool IsSamePosition(XPathNavigator other)
        {
            if (other is ObjectXPathNavigator)
            {
                ObjectXPathNavigator otherNav = (ObjectXPathNavigator)other;

                // if they're in different graphs, they're not the same
                if (this.m_docElem != otherNav.m_docElem)
                {
                    return false;
                }

                // if they're different node types, they can't be the same node!
                if (this.m_nodeType != otherNav.m_nodeType)
                {
                    return false;
                }

                // if they're different elements, they can't be the same node!
                if (this.m_currentElem != otherNav.m_currentElem)
                {
                    return false;
                }

                // are they on different attributes?
                if (this.m_nodeType == XPathNodeType.Attribute &&
                   this.m_valueIndex != otherNav.m_valueIndex)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator"></see>.
        /// </summary>
        /// <param name="other">The <see cref="T:System.Xml.XPath.XPathNavigator"></see> positioned on the node that you want to move to.</param>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the same position as the specified <see cref="T:System.Xml.XPath.XPathNavigator"></see>; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveTo(XPathNavigator other)
        {
            if (other is ObjectXPathNavigator)
            {
                ObjectXPathNavigator otherNav = (ObjectXPathNavigator)other;

                m_docElem = otherNav.m_docElem;
                m_currentElem = otherNav.m_currentElem;
                m_nodeType = otherNav.m_nodeType;
                m_values = otherNav.m_values;
                m_valueIndex = otherNav.m_valueIndex;
                m_nt = otherNav.m_nt;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the attribute with the matching local name and namespace URI.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param>
        /// <param name="namespaceURI">The namespace URI of the attribute; null for an empty namespace.</param>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the attribute; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToAttribute(string localName, string namespaceURI)
        {
            int pos = 0;

            if (m_nodeType != XPathNodeType.Element)
            {
                return false;
            }

            foreach (string name in m_currentElem.AttributeKeys)
            {
                if ((string)name == localName)
                {
                    m_nodeType = XPathNodeType.Attribute;
                    m_valueIndex = pos;

                    return true;
                }

                pos++;
            }

            return false;
        }

        /// <summary>
        /// Moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the first sibling node of the current node.
        /// </summary>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the first sibling node of the current node; false if there is no first sibling, or if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToFirst()
        {
            switch (m_nodeType)
            {
                case XPathNodeType.Element:
                    {
                        m_valueIndex = 0;
                        return true;
                    }

                case XPathNodeType.Attribute:
                    {
                        m_valueIndex = 0;
                        return true;
                    }

                case XPathNodeType.Text:
                    {
                        return true;
                    }
            }

            return false;
        }

        /// <summary>
        /// When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the first attribute of the current node.
        /// </summary>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the first attribute of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToFirstAttribute()
        {
            if (m_nodeType != XPathNodeType.Element)
            {
                return false;
            }

            m_values = m_currentElem.AttributeKeys;
            m_valueIndex = 0;
            m_nodeType = XPathNodeType.Attribute;

            return true;
        }

        /// <summary>
        /// When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the first child node of the current node.
        /// </summary>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the first child node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToFirstChild()
        {
            if (m_nodeType == XPathNodeType.Root)
            {
                // move to the document element
                this.MoveNavigator(m_docElem);
                return true;
            }

            if (m_nodeType != XPathNodeType.Element)
            {
                return false;
            }

            // drop down to the text value (if any)
            if (m_currentElem.HasText)
            {
                m_nodeType = XPathNodeType.Text;
                m_values = null;
                m_valueIndex = -1;

                return true;
            }

            // drop down to the first element (if any)
            IList coll = m_currentElem.Elements;

            if (coll.Count > 0)
            {
                MoveNavigator((ObjectXPathProxy)coll[0]);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves to first namespace.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
        {
            return false;
        }

        /// <summary>
        /// When overridden in a derived class, moves to the node that has an attribute of type ID whose value matches the specified <see cref="T:System.String"></see>.
        /// </summary>
        /// <param name="id">A <see cref="T:System.String"></see> representing the ID value of the node to which you want to move.</param>
        /// <returns>
        /// true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving; otherwise, false. If false, the position of the navigator is unchanged.
        /// </returns>
        public override bool MoveToId(string id)
        {
            return false;
        }

        /// <summary>
        /// Moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the namespace node with the specified namespace prefix.
        /// </summary>
        /// <param name="name">The namespace prefix of the namespace node.</param>
        /// <returns>
        /// true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the specified namespace; false if a matching namespace node was not found, or if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is not positioned on an element node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToNamespace(string name)
        {
            return false;
        }

        /// <summary>
        /// When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the next sibling node of the current node.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the next sibling node; otherwise, false if there are no more siblings or if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToNext()
        {
            if (m_nodeType != XPathNodeType.Element)
            {
                return false;
            }

            ObjectXPathProxy parent = m_currentElem.Parent;

            if (parent == null)
            {
                return false;
            }

            bool foundIt = false;

            foreach (ObjectXPathProxy sib in parent.Elements)
            {
                if (foundIt)
                {
                    MoveNavigator(sib);

                    return true;
                }

                if (m_currentElem == sib)
                {
                    foundIt = true;
                }
            }

            return false;
        }

        /// <summary>
        /// When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the next attribute.
        /// </summary>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the next attribute; false if there are no more attributes. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToNextAttribute()
        {
            if (m_nodeType != XPathNodeType.Attribute)
            {
                return false;
            }

            if (m_valueIndex + 1 >= m_values.Count)
            {
                return false;
            }

            m_valueIndex++;

            return true;
        }

        /// <summary>
        /// Moves to next namespace.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        public override bool MoveToNextNamespace(XPathNamespaceScope scope)
        {
            return false;
        }

        /// <summary>
        /// When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the parent node of the current node.
        /// </summary>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the parent node of the current node; otherwise, false. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToParent()
        {
            if (m_nodeType == XPathNodeType.Root)
            {
                return false;
            }

            if (m_nodeType != XPathNodeType.Element)
            {
                m_nodeType = XPathNodeType.Element;

                return true;
            }

            ObjectXPathProxy parent = m_currentElem.Parent;

            if (parent == null)
            {
                return false;
            }

            MoveNavigator(parent);
            return true;
        }

        /// <summary>
        /// When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the previous sibling node of the current node.
        /// </summary>
        /// <returns>
        /// Returns true if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is successful moving to the previous sibling node; otherwise, false if there is no previous sibling node or if the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is currently positioned on an attribute node. If false, the position of the <see cref="T:System.Xml.XPath.XPathNavigator"></see> is unchanged.
        /// </returns>
        public override bool MoveToPrevious()
        {
            if (m_nodeType != XPathNodeType.Element)
            {
                return false;
            }

            if (m_nodeType != XPathNodeType.Element)
            {
                return false;
            }

            ObjectXPathProxy parent = m_currentElem.Parent;

            if (parent == null)
            {
                return false;
            }

            ObjectXPathProxy previous = null;

            foreach (ObjectXPathProxy sib in parent.Elements)
            {
                if (sib == m_currentElem)
                {
                    if (previous == null)
                    {
                        break;
                    }

                    MoveNavigator(previous);

                    return true;
                }

                previous = sib;
            }

            return false;
        }

        /// <summary>
        /// Moves the <see cref="T:System.Xml.XPath.XPathNavigator"></see> to the root node that the current node belongs to.
        /// </summary>
        public override void MoveToRoot()
        {
            m_nodeType = XPathNodeType.Root;
            m_currentElem = null;
            m_values = null;
            m_valueIndex = -1;
        }

        /// <summary>
        /// Moves the navigator.
        /// </summary>
        /// <param name="nav">The nav.</param>
        private void MoveNavigator(ObjectXPathProxy nav)
        {
            m_nodeType = XPathNodeType.Element;
            m_currentElem = nav;
            m_values = nav.Elements;
            m_valueIndex = -1;
        }
    }
}
