using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Console
{
    public class Argument : IFileHandling
    {
        string[] _clArguments = null;

        public Argument(string[] clArguments)
        {
            _clArguments = clArguments;
        }

        public string[] ParseFileData(string filePath)
        {
            return new string[] { filePath };
        }

        #region  ' IFileHandling '

        public void DisplayData(List<string> parsedData)
        {
            parsedData.ForEach(vi => Console.WriteLine("{0}", vi));
        }

        public string GetFilePath(string clArgumentNameValue)
        {
            string filePath = _clArguments.SkipWhile(a => string.Compare(a, clArgumentNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty("")
                .First()
                .ToString();

            return filePath;
        }

        public string[] GetParsedData(string clArgumentNameValue) => ParseFileData(GetFilePath(clArgumentNameValue)).ToArray();

        #endregion
    }
}
