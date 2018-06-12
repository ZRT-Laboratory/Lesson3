using Project.Interface;
using Project.Xml;
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
            if (clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0))
            {
                //use interface
                IFileHandling ifhJson = null;
                IFileHandling ifhXml = new XmlParser();

                string[] fileData = ifhJson?.GetParsedData(File.ReadAllText(GetFilePath("-json"))).Concat(ifhXml?.GetParsedData(File.ReadAllText(GetFilePath("-xml")))).ToArray();

                List<string> parsedData = fileData?.OrderBy(fd => fd == null).ThenBy(fd => fd).ToList();

                //display the list and replace nulls with No Value
                parsedData.ForEach(pd => Console.WriteLine("{0}", pd ?? "No Value"));
            }
            else
            {
                throw new ArgumentException("Invalid arguments.");
            }

            string GetFilePath(string argumentNameValue)
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
        }      
    }
}
