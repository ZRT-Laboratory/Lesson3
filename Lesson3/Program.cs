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
            if (clArguments.HaveValidArguments())
            {
                List<string> parsedData = clArguments.GetSortedFileDataFromArguments().ToList();
                parsedData?.ForEach(pd => Console.WriteLine($"{pd}"));
            }
        }      
    }
}
