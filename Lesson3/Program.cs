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
                string[] fileData = clArguments.GetJsonData().Concat(clArguments.GetXmlData()).ToArray();

                List<string> parsedData = fileData.SortWithNullsToBottom().ToList();
                parsedData.ForEach(pd => Console.WriteLine($"{pd}"));
            }
        }      
    }
}
