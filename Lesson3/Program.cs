using Project_Interface;
using Project_Json;
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
            bool haveValidArguments = clArguments.Any(a => String.Compare(a, "-json", true) == 0);

            if (haveValidArguments)
            {
                IFileHandling ifh = new ArgumentJson();

                //create a list then order it so nulls are last in the list
                List<string> allItems = ifh.GetParsedData(clArguments)
                    .OrderBy(fh => fh)
                    .ToList()
                    .OrderBy(ai => ai == null)
                    .ToList();

                //display the list and replace nulls with No Value
                ifh.DisplayData(allItems);
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}
