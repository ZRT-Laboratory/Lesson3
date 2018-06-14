using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            string[] fileData = Array.Empty<string>();
            
            List<string> parsedData = fileData.GetSortedFileDataFromArguments(clArguments).ToList();

            parsedData.ForEach(pd => Console.WriteLine("{0}", pd));
        }      
    }
}
