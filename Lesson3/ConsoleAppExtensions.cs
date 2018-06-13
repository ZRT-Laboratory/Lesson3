using Project.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.ConsoleApp
{
    public static class ConsoleAppExtensions
    {
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="parsedData">sorted string collection of file data</param>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static IEnumerable<string> GetFileData(this IEnumerable<string> parsedData, string[] clArguments)
        {
            bool haveJsonArgument = clArguments.Any(a => string.Compare(a, "-json", true) == 0);
            bool haveXmlArgument = clArguments.Any(a => string.Compare(a, "-xml", true) == 0);
            
            if (haveJsonArgument || haveXmlArgument)
            {
                //use interface
                IFileHandling ifhJson = null;
                IFileHandling ifhXml = null;

                string jsonData = string.Empty;
                string xmlData = string.Empty;

                if (haveJsonArgument)
                {
                    jsonData = File.ReadAllText(GetFilePath(clArguments, "-json"));
                }

                if (haveXmlArgument)
                {
                    xmlData = File.ReadAllText(GetFilePath(clArguments, "-xml"));
                }

                parsedData = ifhJson?.GetParsedData(jsonData)
                    .Concat(ifhXml?.GetParsedData(xmlData))
                    .SortNullValuesToBottom()
                    .ReplaceNullsWithStringValue("No Value")
                    .ToList();
            }
            else
            {
                throw new ArgumentException("Invalid arguments.");
            }

            return parsedData;
        }

        /// <summary>
        /// GetFilePath
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <param name="argumentNameValue">the command line argument being searched for. Example: -json</param>
        /// <returns></returns>
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

        /// <summary>
        /// SortNullValuesToBottom
        /// </summary>
        /// <param name="arrayItems"></param>
        /// <returns>sorted collection with nulls at the bottom of the collection</returns>
        private static IEnumerable<string> SortNullValuesToBottom(this IEnumerable<string> arrayItems)
        {
            return arrayItems.OrderBy(fd => fd == null).ThenBy(fd => fd);            
        }

        /// <summary>
        /// ReplaceNullsWithStringValue
        /// </summary>
        /// <param name="listItems"></param>
        /// <param name="stringValue"></param>
        /// <returns>collection with all nulls replaced with parameter</returns>
        private static IEnumerable<string> ReplaceNullsWithStringValue(this IEnumerable<string> listItems, string stringValue)
        {
            return listItems.Select(li => li ?? stringValue);
        }

    }
}
