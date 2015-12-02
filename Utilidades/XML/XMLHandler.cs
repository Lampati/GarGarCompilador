using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utilidades.XML
{
    public class XMLElement
    {
        public string space;
        public string title;
        public string value;

        public XMLElement parent;
        public List<XMLElement> childs;
        public List<XMLproperty> properties;

        public XMLElement()
        {
            this.space = "";
            this.title = "";
            this.value = "";
            this.parent = null;
            this.childs = new List<XMLElement>();
            this.properties = new List<XMLproperty>();
        }

        public XMLElement(string space, string title, string value = "", XMLElement parent = null)
        {
            this.space = space;
            this.title = title;
            this.value = value;
            this.childs = new List<XMLElement>();
            this.properties = new List<XMLproperty>();

            if (parent != null) parent.AddChild(this);
            else this.parent = null;
        }

        public void AddProperty(string space, string title, string value)
        {
            properties.Add(new XMLproperty(space, title, value));
        }

        public void AddChild(XMLElement element)
        {
            this.childs.Add(element);
            element.parent = this;
        }

        public string FullTitle()
        {
            string str = "";
            if (space != "") str += space + ":";
            str += title;
            return str;
        }

        public string Get(int level = 0)
        {
            bool empty = value == "" && childs.Count == 0;

            string str = "";

            if (level != 0)
            {
                str += "\n";
                for (int i = 0; i < level; i++) str += "\t";
            }

            str += "<";
            str += FullTitle();
            if (properties.Count > 0)
            {
                foreach (XMLproperty p in properties)
                {
                    str += "\n";
                    for (int i = 0; i < level + 1; i++) str += "\t";
                    str += p.Get();
                }
            }
            if (!empty)
            {
                str += ">";

                if (childs.Count == 0)
                {
                    str += value;
                }
                else
                {
                    foreach (XMLElement e in childs)
                    {
                        str += e.Get(level + 1);
                    }
                    str += "\n";
                    for (int i = 0; i < level; i++) str += "\t";
                }

                str += "</";
                if (space != "") str += space + ":";
                str += title + ">";
            }
            else
            {
                str += "/>";
            }

            return str;
        }

        public XMLElement FindFirst(string title)
        {
            XMLElement element = null;
            foreach (XMLElement child in this.childs)
            {
                if (child.title == title)
                {
                    element = child;
                    break;
                }
                else
                {
                    element = child.FindFirst(title);
                    if (element != null) break;
                }
            }
            return element;
        }

        public XMLElement FindFirstPrefix(string title)
        {
            XMLElement element = null;
            foreach (XMLElement child in this.childs)
            {
                if (child.title.StartsWith(title))
                {
                    element = child;
                    break;
                }
                else
                {
                    element = child.FindFirstPrefix(title);
                    if (element != null) break;
                }
            }
            return element;
        }
    }

    public class XMLproperty
    {
        public string space;
        public string title;
        public string value;

        public XMLproperty(string space, string title, string value)
        {
            this.space = space;
            this.title = title;
            this.value = value;
        }

        public string Get()
        {
            string str = "";
            if (space != "")
            {
                str += space + ":";
            }
            str += title + "=\"" + value.Replace("\"", "&quot") + "\"";
            return str;
        }
    }

    public class XMLCreator
    {
        public XMLElement root;
        private XMLElement pointer;

        public XMLCreator()
        {
            root = null;
            pointer = null;
        }

        public void AddElement()
        {
            if (pointer == null)
            {
                root = new XMLElement();
                pointer = root;
            }
            else
            {
                XMLElement item = new XMLElement();
                pointer.AddChild(item);
                pointer = item;
            }
        }

        public void SetNamespace(string space)
        {
            if (pointer != null) pointer.space = space;
        }

        public void SetTitle(string title)
        {
            if (pointer != null) pointer.title = title;
        }

        public void SetTitle(string space, string title)
        {
            if (pointer != null)
            {
                pointer.space = space;
                pointer.title = title;
            }
        }

        public void SetValue(string value)
        {
            if (pointer != null) pointer.value = value;
        }

        public void AddProperty(string title, string value)
        {
            if (pointer != null)
            {
                pointer.AddProperty("", title, value);
            }
        }

        public void AddProperty(string space, string title, string value)
        {
            if (pointer != null)
            {
                pointer.AddProperty(space, title, value);
            }
        }

        public void LevelUp()
        {
            if (pointer != null && pointer.parent != null)
                pointer = pointer.parent;
        }

        public string Get()
        {
            if (root != null) return root.Get();
            return "";
        }
    }

    public class XMLReader
    {
        public XMLReader()
        {
        }

        public XMLElement Read(string xmlRead)
        {
            xmlRead = xmlRead.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            XMLElement root = new XMLElement();
            FillElement(root, xmlRead);

            return root;
        }

        private void FillElement(XMLElement parent, string data)
        {
            int childs = 0;
            int idx = 0;

            while (idx < data.Length)
            {
                int ini = data.IndexOf('<', idx);
                int end = data.IndexOf('>', idx);
                if (ini < 0 || end < 0 || ini >= end)
                {
                    // no more data
                    break;
                }

                childs++;

                String str = data.Substring(ini + 1, end - ini - 1);
                bool empty = data.ElementAt(end - 1) == '/';

                XMLElement element = new XMLElement();
                parent.AddChild(element);
                ExtractHeader(element, str);

                if (!empty)
                {
                    try
                    {
                        string strClose = "</" + element.FullTitle() + ">";
                        int dataEnd = data.IndexOf(strClose, idx);
                        string newData = data.Substring(end + 1, dataEnd - end - 1);
                        FillElement(element, newData);
                        idx = dataEnd + strClose.Length;
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    idx = end + 1;
                }
            }

            if (childs == 0)
            {
                parent.value = data;
            }
        }

        private int FirstStringLenght(string str)
        {
            int spc = str.IndexOf(' ');
            int bar = str.IndexOf('/');
            int ret = str.IndexOf('\r');
            int nln = str.IndexOf('\n');
            int len = str.Length;
            if (spc >= 0 && spc < len) len = spc;
            if (bar >= 0 && bar < len) len = bar;
            if (ret >= 0 && ret < len) len = ret;
            if (nln >= 0 && nln < len) len = nln;
            return len;
        }

        private string TrimBeginning(string str)
        {
            while (str.Length >= 1)
            {
                if (str[0] == ' ') str = str.Substring(1);
                else if (str[0] == '\t') str = str.Substring(1);
                else if (str[0] == '\n') str = str.Substring(1);
                else if (str[0] == '\r') str = str.Substring(1);
                else break;
            }
            return str;
        }

        private int FirstPropertyLength(string str)
        {
            int equ = str.IndexOf('=');
            if (equ > 1)
            {
                char del = str[equ + 1];
                int end = str.IndexOf(del, equ + 2);
                return end + 1;
            }
            else
            {
                return 0;
            }
        }

        private void ExtractHeader(XMLElement element, string data)
        {
            int len = FirstStringLenght(data);

            string title = data.Substring(0, len);
            int col = title.IndexOf(':');
            if (col > 0)
            {
                element.space = title.Substring(0, col);
                element.title = title.Substring(col + 1);
            }
            else
            {
                element.title = title;
            }

            string newData;
            newData = data.Substring(len);
            do
            {
                newData = TrimBeginning(newData);
                len = FirstPropertyLength(newData);
                if (len >= 4)
                {
                    int equ = newData.IndexOf('=');
                    string propTitle = newData.Substring(0, equ);
                    string propSpace = "";
                    int pCol = propTitle.IndexOf(':');
                    if (pCol > 0)
                    {
                        propSpace = propTitle.Substring(0, pCol);
                        propTitle = propTitle.Substring(pCol + 1);
                    }

                    char delimiter = '"';
                    string oldS = "&quot";
                    string newS = "\"";
                    if (newData[equ + 1] != '"')
                    {
                        delimiter = '\'';
                        oldS = "&apos";
                        newS = "'";
                    }
                    string propValue = newData.Substring(equ + 2);
                    propValue = propValue.Substring(0, propValue.IndexOf(delimiter));
                    propValue = propValue.Replace(oldS, newS);
                    element.AddProperty(propSpace, propTitle, propValue);
                    newData = newData.Substring(len);
                }
                else
                {
                    break;
                }
            }
            while (newData.Length > 0);
        }

        public static string Escape(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                xml = xml.Replace("&", "&amp;");
                xml = xml.Replace("<", "&lt;");
                xml = xml.Replace(">", "&gt;");
                xml = xml.Replace("\"", "&quot;");
                xml = xml.Replace("'", "&apos;");
            }
            return xml;
        }

        public static string Unescape(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                // replace entities with literal values
                xml = xml.Replace("&apos;", "'");
                xml = xml.Replace("&quot;", "\"");
                xml = xml.Replace("&gt;", ">");
                xml = xml.Replace("&lt;", "<");
                xml = xml.Replace("&amp;", "&");
            }
            return xml;
        }
    }
}
