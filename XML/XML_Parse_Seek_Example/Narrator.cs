using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace seek_ex
{
    class Narrator
    {
        public Program main = new Program();

        public void init_System(List<string> group, IEnumerable<string> item)
        {
            Console.WriteLine("==== Select Element ====");
            Console.WriteLine("1. Group");
            Console.WriteLine("2. Item");
            Console.WriteLine();

            Console.Write("Selection : ");
            string sel = Console.ReadLine();

            switch (Convert.ToInt32(sel))
            {
                case 1 :
                    startGroup("Group", group);
                    break;
                case 2:
                    startItem("item", item);
                    break;
            }
        }

        public void startGroup(string target, List<string> group)
        {
            Console.WriteLine("==== Select" + target + "====");
            for (int i = 0; i < group.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, group[i]); 
            }
            Console.WriteLine();

            Console.Write("Selection : ");
            string sel = Console.ReadLine();

            main.Seek_Group(target, group[Convert.ToInt32(sel) - 1]);
        }

        public void startItem(string target, IEnumerable<string> items)
        {
            int count = 0;
            List<string> item_list = new List<string>();

            Console.WriteLine("==== Select" + target + "====");
            foreach(string item in items)
            {
                count++;
                Console.WriteLine("{0}. {1}", count, item);
                item_list.Add(item);
            }
            Console.WriteLine();

            Console.Write("Selection : ");
            string sel = Console.ReadLine();

            main.Seek_Item(target, item_list[Convert.ToInt32(sel) - 1]);
        }
    }
}
