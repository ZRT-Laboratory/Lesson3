using Project.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] clArguments)
        {
            if (clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0))
            {
                //use interface
                IFileHandling ifhJson = GetJsonParser();
                IFileHandling ifhXml = GetXmlParser();

                //create a list then order it so nulls are last in the list
                List<string> parsedData = ifhJson?.GetParsedData(GetFileData(GetFilePath("-json")))
                    .Concat(ifhXml?.GetParsedData(GetFileData(GetFilePath("-xml"))))
                    .OrderBy(ifh => ifh)
                    .ThenBy(ifh => ifh == null)
                    .ToList();

                parsedData?.ForEach(pd => Console.WriteLine("{0}", pd));
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

            IFileHandling GetJsonParser() => null;

            IFileHandling GetXmlParser() => null;            
        }      
    }
}
