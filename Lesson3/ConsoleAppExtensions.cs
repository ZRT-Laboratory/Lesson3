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
        /// GetSortedFileData
        /// </summary>
        /// <param name="parsedData">sorted string collection of file data</param>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static IEnumerable<string> GetSortedFileData(this IEnumerable<string> parsedData, string[] clArguments)
        {
            bool haveJson = clArguments.Any(a => string.Compare(a, "-json", true) == 0);
            bool haveXml = clArguments.Any(a => string.Compare(a, "-xml", true) == 0);
            
            //if we have valid json or xml return a sorted array of that data
            if (haveJson || haveXml)
            {
                //use interface
                IFileHandling ifhJson = null;
                IFileHandling ifhXml = null;

                string jsonData = string.Empty;
                string xmlData = string.Empty;

                //get json file data
                if (haveJson)
                {
                    jsonData = File.ReadAllText(GetFilePath(clArguments, "-json"));
                }

                //get xml file data
                if (haveXml)
                {
                    xmlData = File.ReadAllText(GetFilePath(clArguments, "-xml"));
                }

                //parse and merge the data, sort null values to the bottom then replace null values with string literal 'No Value'
                parsedData = ifhJson?.GetParsedData(jsonData)
                    .Concat(ifhXml?.GetParsedData(xmlData))
                    .SortNullValuesToBottom()
                    .ReplaceNullsWithStringValue("No Value")
                    .ToArray();

                //if null then return empty array
                parsedData = parsedData ?? Array.Empty<string>();
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
                throw new FileNotFoundException($"Invalid file name - {filePath}.");
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
