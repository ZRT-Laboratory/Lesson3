using Project.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.ConsoleApp
{
    public class Program
    {
        private static string GetFilePath(string[] clArguments, string argumentNameValue)
        {
            string filePath = clArguments.SkipWhile(a => string.Compare(a, argumentNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty("")
                .First()
                .ToString();

            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {
                throw new FileNotFoundException("Invalid file name.", filePath);
            }

            return filePath;
        }

        [STAThread]
        public static void Main(string[] clArguments)
        {
            if (clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0))
            {
                //use interface
                IFileHandling ifhJson = null;
                IFileHandling ifhXml = null;

                string jsonData = string.Empty;
                string xmlData = string.Empty;

                List<string> parsedData = ifhJson?.GetParsedData(File.ReadAllText(GetFilePath(clArguments, "-json")))
                    .Concat(ifhXml?.GetParsedData(File.ReadAllText(GetFilePath(clArguments, "-xml"))))
                    .SortNullValuesToBottom()
                    .ReplaceNullsWithStringValue("No Value")
                    .ToList();

                parsedData?.ForEach(pd => Console.WriteLine("{0}", pd));
            }
            else
            {
                throw new ArgumentException("Invalid arguments.");
            }
        }      
    }
}
