using Project.Interface;
using Project.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.ConsoleApp
{
    public static class ConsoleAppExtensions
    {
        /// <summary>
        /// GetSortedFileDataFromArguments
        /// </summary>
        /// <param name="sortedData">sorted collection of file data</param>
        /// <param name="clArguments">command line arguments</param>
        /// <returns>an array of sorted file data or an empty array</returns>
        public static IEnumerable<string> GetSortedFileDataFromArguments(this IEnumerable<string> sortedData, string[] clArguments)
        {
            bool haveJson = clArguments.Any(a => string.Compare(a, "-json", true) == 0);
            bool haveXml = clArguments.Any(a => string.Compare(a, "-xml", true) == 0);
            
            //if we have valid json or xml return a sorted array of that data
            if (haveJson || haveXml)
            {
                //use interface
                IFileHandling ifhJson = null;
                IFileHandling ifhXml = new XmlParser();

                //json data
                string jsonData = haveJson ? File.ReadAllText(GetFilePathFromArgument(clArguments, "-json")) : string.Empty;
                string[] jsonParsed = ifhJson != null ? ifhJson.GetParsedData(jsonData) : Array.Empty<string>();

                //xml data
                string xmlData = haveXml ?  File.ReadAllText(GetFilePathFromArgument(clArguments, "-xml")) : string.Empty;
                string[] xmlParsed = ifhXml != null ? ifhXml.GetParsedData(xmlData) : Array.Empty<string>();

                //parse and merge the data, sort null values to the bottom then replace null values with string literal 'No Value'
                sortedData = jsonParsed
                    .Concat(xmlParsed)
                    .SortNullValuesToBottom()
                    .ReplaceNullsWithStringValue("No Value")
                    .ToArray();                

                //if null then return empty array
                sortedData = sortedData ?? Array.Empty<string>();
            }
            else
            {
                throw new ArgumentException("Invalid arguments.");
            }
            
            return sortedData;
        }

        /// <summary>
        /// GetFilePathFromArgument
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <param name="argumentNameValue">the command line argument being searched for. Example: -json</param>
        /// <returns>file path or empty string if can't find that argumentnamevalue</returns>
        private static string GetFilePathFromArgument(string[] clArguments, string argumentNameValue)
        {
            string filePath = clArguments.SkipWhile(a => string.Compare(a, argumentNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty(string.Empty)
                .First()
                .ToString();

            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {
                throw new FileNotFoundException($"Invalid file name - {filePath}.");
            }

            return filePath;
        }

        /// <summary>
        /// ReplaceNullsWithStringValue
        /// </summary>
        /// <param name="items"></param>
        /// <param name="stringValue">string value used to replace null values</param>
        /// <returns>collection with all nulls replaced with parameter</returns>
        public static IEnumerable<string> ReplaceNullsWithStringValue(this IEnumerable<string> items, string stringValue)
        {
            return items.Select(li => li ?? stringValue);
        }

        /// <summary>
        /// SortNullValuesToBottom
        /// </summary>
        /// <param name="items"></param>
        /// <returns>sorted collection with nulls at the bottom of the collection</returns>
        public static IEnumerable<string> SortNullValuesToBottom(this IEnumerable<string> items)
        {
            return items.OrderBy(fd => fd == null).ThenBy(fd => fd);
        }

    }
}
