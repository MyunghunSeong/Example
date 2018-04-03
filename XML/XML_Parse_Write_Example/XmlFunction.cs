using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XML_Parse_Write_Example
{
    class XmlFunction
    {
        public Senario senario = new Senario();
        public XmlDocument xmlDoc = new XmlDocument();

        public void Add_Xml_File()
        {
            try
            {
                string EleName = string.Empty;
                string EleValue = string.Empty;

                xmlDoc.Load("data.xml");

                EleName = senario.InputElementName();

                XmlElement newElement = xmlDoc.CreateElement(EleName);

                EleValue = senario.InputElementValue();

                newElement.InnerText = EleValue;
                xmlDoc.DocumentElement.AppendChild(newElement);

                XmlTextWriter writer = new XmlTextWriter("data.xml", null);
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);
                writer.Close();
            }
            catch
            {
                throw new Exception();
            }
        }

        public void Modify_Xml_File()
        {
            try
            {
                xmlDoc.Load("data.xml");

                XmlNodeList node_list = xmlDoc.DocumentElement.ChildNodes;

                int selection = senario.SelectElement(node_list);

                string value = senario.ModyfyValue();

                xmlDoc.DocumentElement.ChildNodes[selection - 1].InnerText = value;

                xmlDoc.Save("data.xml");
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
