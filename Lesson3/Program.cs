using Project_Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Console
{
    class Program
    {
        static void Main(string[] clArguments)
        {
            IFileHandling ifh = new Argument();
            List<string> allItems = new List<string>();

            if (clArguments.Any(a => String.Compare(a, "-json", true) == 0))
            {
                allItems.Add(ifh.GetFilePath(clArguments, "-json"));
            }

            if (clArguments.Any(a => String.Compare(a, "-xml", true) == 0))
            {
                allItems.Add(ifh.GetFilePath(clArguments, "-xml"));
            }

            allItems.OrderBy(vi => vi);
            ifh.DisplayData(allItems);
        }
    }
}
