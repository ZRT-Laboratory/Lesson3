using Project_Xml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            bool validArguments = clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0);

            if (validArguments)
            {
                IFileHandling ifhXml =  new ArgumentXml();

                //create a list then order it so nulls are last in the list
                List<string> allItems = ifhXml.GetParsedData(clArguments)
                    .OrderBy(ifh => ifh)
                    .ToList()
                    .OrderBy(ai => ai == null)
                    .ToList();

                //display the list and replace nulls with No Value
                allItems.ForEach(i => Console.WriteLine("{0}", i ?? "No Value"));
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}
