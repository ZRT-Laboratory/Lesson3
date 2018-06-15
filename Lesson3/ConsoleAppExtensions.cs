using Project.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.ConsoleApp
{
    public static class ConsoleAppExtensions
    {
        const string _json = "-json";
        const string _xml = "-xml";
        const string _nullReplacement = "No Value";

        /// <summary>
        /// GetSortedFileDataFromArguments
        /// </summary>
        /// <param name="sortedData">sorted collection of file data</param>
        /// <param name="clArguments">command line arguments</param>
        /// <returns>an array of sorted file data or an empty array</returns>
        public static string[] GetSortedFileDataFromArguments(this string[] clArguments)
        {
            if (!clArguments.HaveValidArgument(_json) || !clArguments.HaveValidArgument(_xml))
            {
                throw new ArgumentException("Invalid arguments.");
            }

            //json data
            string[] jsonParsed = clArguments.GetFileData(_json, null);

            //xml data
            string[] xmlParsed = clArguments.GetFileData(_xml, null);

            //parse and merge the data, sort null values to the bottom then replace null values with string literal 'No Value'
            var sortedData = jsonParsed
                .Concat(xmlParsed)
                .SortNullValuesToBottom()
                .ReplaceNullsWithStringValue(_nullReplacement)
                .ToArray();

            return sortedData;
        }

        private static bool HaveValidArgument(this string[] clArguments, string argumentType)
        {
            return clArguments.Any(a => string.Compare(a, argumentType, true) == 0);
        }


        /// <summary>
        /// GetFileData
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <param name="argumentType">the command line argument being searched for. Example: -json</param>
        /// <param name="ifh">interface for the parser type example JsonParser</param>
        /// <returns></returns>
        private static string[] GetFileData(this string[] clArguments, string argumentType, IFileHandling ifh)
        {
            string fileData = HaveValidArgument(clArguments, argumentType) ? File.ReadAllText(GetFilePathFromArgument(clArguments, argumentType)) : string.Empty;
            string[] parsedData = ifh != null ? ifh.GetParsedData(fileData) : Array.Empty<string>();

            return parsedData;
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
