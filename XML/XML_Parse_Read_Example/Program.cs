using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CMS_G.sub.Err;

namespace XML_Parse_Example
{
    class Program
    {
        static void Read_XML_File()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("group.xml");

                XmlNode root = xmlDoc.DocumentElement;

                SetNode(root);
            
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }

        static void SetNode(XmlNode parents)
        {
            XmlNodeList parents_list = parents.ChildNodes;
            List<String> nodeName = new List<String>();
            List<String> nodeValue = new List<String>();
            Dictionary<string, string> nodeInfo = new Dictionary<string, string>();

            try
            {
                foreach (XmlNode node in parents_list)
                {
                    XmlAttributeCollection nodeAttrs = node.Attributes;

                    foreach (XmlAttribute nodeattr in nodeAttrs)
                    {
                        nodeName.Add(nodeattr.Name);
                        nodeValue.Add(nodeattr.Value);
                        SetDictionary(ref nodeInfo, ref nodeName, ref nodeValue);
                    }

                    ShowElement(nodeName, nodeValue, node);

                    nodeName = new List<string>();
                    nodeValue = new List<string>();

                    if (node != null)
                        SetNode(node);
                }
            }
            catch (NullReferenceException err)
            {
                Console.WriteLine(err.Message);
            }
        }

        static void ShowElement(List<string> Name, List<string> Value, XmlNode element)
        {
            Console.WriteLine("Element : " + element.Name);
            Console.WriteLine();
            for(int i = 0; i < Name.Count; i++)
            {
                Console.WriteLine("Attribute[{0}] : {1}", Name[i], Value[i]);
            }
            Console.WriteLine();
        }

        static void SetDictionary(ref Dictionary<string, string> dic, ref List<string> key, ref List<string> value)
        {
            for (int i = 0; i < key.Count; i++)
            {
                dic[key[i]] = value[i];
            }
        }

        static void Main(string[] args)
        {
            Read_XML_File();
        }
    }
}
