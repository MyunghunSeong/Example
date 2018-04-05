using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace seek_ex
{
    class Program
    {
        public void Seek_Group(string targetName, string target)
        {
            XElement root = XElement.Load("group.xml");
            IEnumerable<XElement> group =
                from el in root.Elements(targetName)
                where (string)el.Attribute("Name") == target
                select el;

            foreach (XElement el in group)
                Console.WriteLine(el);
        }

        public void Seek_Item(string targetName, string target)
        {
            XElement root = XElement.Load("group.xml");
            IEnumerable<XElement> item =
                from el in root.Descendants(targetName)
                where (string)el.Attribute("Type") == target
                select el;

            foreach (XElement el in item)
                Console.WriteLine(el);
        }

        static void Find_Element()
        {
            Narrator narrator = new Narrator();
            XElement root = XElement.Load("group.xml");
            List<string> GroupAttrValue = new List<string>();
            List<string> ItemAttrValue = new List<string>();

            IEnumerable<XAttribute> group =
                from Gel in root.Elements("Group").Attributes()
                select Gel;

            IEnumerable<XAttribute> item =
                from Iel in root.Descendants("item").Attributes()
                select Iel;

            foreach (XAttribute Iel in item)             
                 ItemAttrValue.Add(Iel.Value);

            var distin_Item = ItemAttrValue.Select(a => a).Distinct();

            foreach (XAttribute Gel in group)
                GroupAttrValue.Add(Gel.Value);

            narrator.init_System(GroupAttrValue, distin_Item);
        }

        static void Main(string[] args)
        {
            Find_Element();
        }
    }
}
