using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XML_Parse_Write_Example
{
    class Senario
    {
        public string InputElementName()
        {
            Console.Write("Input Element Name : ");
            string name = Console.ReadLine();

            return name;
        }

        public string InputElementValue()
        {
            Console.Write("input Element Value : ");
            string value = Console.ReadLine();

            return value;
        }

        public int SelectElement(XmlNodeList list)
        {
            int count = 0;
            string selection = string.Empty;

            foreach (XmlNode node in list)
            {
                Console.WriteLine("=== Element List ===");
                Console.WriteLine("{0}. {1}", ++count, node.Name);
            }

            try
            {
                Console.Write("Selection : ");
                selection = Console.ReadLine();
                Console.WriteLine();
            }
            catch (Exception err)
            {
                Console.WriteLine("잘못된 문자열 입력!!");
            }

            return Convert.ToInt32(selection);
        }

        public string ModyfyValue()
        {
            Console.Write("Modify Value : ");
            string value = Console.ReadLine();

            return value;
        }

        public int SelectionFunc()
        {
            Console.WriteLine("=== Selection Function ===");
            Console.WriteLine("1. Add Xml_File ");
            Console.WriteLine("2. Modify Xml_File ");
            Console.WriteLine("3. Exit");
            Console.WriteLine();

            Console.Write("selection : ");

            string selection = Console.ReadLine();
            Console.WriteLine();

            return Convert.ToInt32(selection);
        }
    }
}
