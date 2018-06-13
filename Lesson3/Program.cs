using System;
using System.Collections.Generic;

namespace Project.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            List<string> parsedData = new List<string>();

            parsedData.GetSortedFileData(clArguments);
            parsedData.ForEach(pd => Console.WriteLine("{0}", pd));
        }      
    }
}
