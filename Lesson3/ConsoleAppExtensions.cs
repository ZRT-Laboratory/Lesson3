using Project.Interface;
using Project.Json;
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

        /// <summary>
        /// SortNullsToBottomAndReplaceWithStringValue
        /// </summary>
        /// <param name="items">string collection of items to sort</param>
        /// <param name="nullReplacement">string literal to replace nulls with. Example: "No Value"/param>
        /// <returns>an array of sorted items with nulls at the bottom and replaced with a string literal</returns>
        public static IEnumerable<string> SortNullsToBottomAndReplaceWithStringValue(this IEnumerable<string> items, string nullReplacement)
        {
            return items
                .SortNullValuesToBottom()
                .ReplaceNullsWithStringValue(nullReplacement);
        }

        /// <summary>
        /// GetJsonData
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static string[] GetJsonData(this string[] clArguments)
        {
            string jsonFilePath = clArguments.GetFilePathFromArgument(_json);
            return jsonFilePath.GetFileData(new JsonParser());
        }

        /// <summary>
        /// GetXmlData
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static string[] GetXmlData(this string[] clArguments)
        {
            string xmlFilePath = clArguments.GetFilePathFromArgument(_xml);
            return xmlFilePath.GetFileData(null);
        }

        /// <summary>
        /// GetFileData
        /// </summary>
        /// <param name="filePath">file location for data to be parsed</param>
        /// <param name="ifh">interface for the parser type example JsonParser</param>
        /// <returns>returns properly parsed file data</returns>
        private static string[] GetFileData(this string filePath, IFileHandling ifh)
        {
            string[] parsedData = null;

            if (ifh != null && !string.IsNullOrWhiteSpace(filePath))
            {
                string fileData = File.ReadAllText(filePath);
                parsedData = ifh.GetParsedData(fileData);
            }

            return parsedData ?? Array.Empty<string>();
        }

        /// <summary>
        /// GetFilePathFromArgument
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <param name="argumentNameValue">the command line argument type being searched for. Example: -json</param>
        /// <returns>file path or empty string if can't find that argumentnamevalue</returns>
        private static string GetFilePathFromArgument(this string[] clArguments, string argumentNameValue)
        {
            string filePath = clArguments.SkipWhile(a => string.Compare(a, argumentNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty(string.Empty)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {
                throw new FileNotFoundException($"Invalid file name - {filePath}.");
            }

            return filePath;
        }

        /// <summary>
        /// HaveValidArgument
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static bool HaveValidArguments(this string[] clArguments)
        {
            return clArguments.Any(a => string.Equals(a, _json) || string.Equals(a, _xml));
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
