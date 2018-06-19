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
                List<string> parsedData = clArguments
                    .GetJsonData()
                    .Concat(clArguments.GetXmlData())
                    .SortNullValuesToBottom()
                    .ReplaceNullsWithStringValue("No Value")
                    .ToList();

                parsedData.ForEach(pd => Console.WriteLine($"{pd}"));
            }
        }      
    }
}
