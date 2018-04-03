using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XML_Parse_Write_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                XmlFunction xmlFunc = new XmlFunction();

                xmlFunc.Add_Xml_File();
                //xmlFunc.Modify_Xml_File();
  
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
