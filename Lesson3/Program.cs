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
        public static List<string> GetSortedData(string[] fileData)
        {
            List<string> sortedData = new List<string>();

            //create a list then order it so nulls are last in the list
            if (fileData != null)
            {
                sortedData = fileData
                    .OrderBy(fd => fd == null)
                    .ThenBy(fd => fd)
                    .ToList();
            }

            return sortedData;
        }

        [STAThread]
        public static void Main(string[] clArguments)
        {
            if (clArguments.Any(a => String.Compare(a, "-xml", true) == 0))
            {
                IFileHandling ifhXml = new XmlParser();

                //get file data sorted with nulls at the end of the list
                List<string> parsedData = GetSortedData(ifhXml?.GetParsedData(GetFileData(GetFilePath("-xml")))).ToList();

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
                    throw new ArgumentException("Invalid file name", argumentNameValue);
                }                

                return filePath;
            }

            string GetFileData(string filePath)
            {
                string fileData = string.Empty;

                if (!string.IsNullOrEmpty(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        fileData = reader.ReadToEnd();
                    }
                }
                
                return fileData;
            }
        }
    }
}
