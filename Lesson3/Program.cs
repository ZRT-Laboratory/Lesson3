using Project.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.ConsoleApp
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] clArguments)
        {
            List<string> parsedData = new List<string>();
            parsedData.GetFileData(clArguments);
            parsedData.ForEach(pd => Console.WriteLine("{0}", pd));
        }      
    }
}
