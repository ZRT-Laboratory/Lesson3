using Project.ConsoleApp.Parser;
using Project.Interface;
using Project.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.ConsoleApp
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] clArguments)
        {
            if (clArguments.Any(a => String.Compare(a, "-json", true) == 0))
            {
                IFileHandling ifh = new JsonParser(clArguments);

                //create a list then order it so nulls are last in the list
                List<string> parsedData = ifh.GetParsedData(GetFilePath(clArguments, "-json"))
                    .OrderBy(fh => fh)
                    .ToList()
                    .OrderBy(ai => ai == null)
                    .ToList();

                //display the list and replace nulls with No Value
                parsedData.ForEach(vi => Console.WriteLine("{0}", vi ?? "No Value"));
            }
            else
            {
                throw new ArgumentException("Invalid arguments.");
            }

            string GetFilePath(string[] arguments, string argumentNameValue)
            {
                string filePath = clArguments.SkipWhile(a => string.Compare(a, argumentNameValue, true) != 0)
                    .Skip(1)
                    .DefaultIfEmpty("")
                    .First()
                    .ToString();

                return filePath;
            }
        }
    }
}
