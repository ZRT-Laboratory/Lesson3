using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Console
{
    public class Argument : IFileHandling
    {
        #region  ' IFileHandling '

        public void DisplayData(List<string> valueItems)
        {
            valueItems.ForEach(vi => Console.WriteLine("{0}", vi));
        }

        public string GetFilePath(string[] clArguments, string clNameValue)
        {
            string filePath = clArguments.SkipWhile(a => string.Compare(a, clNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty("")
                .First()
                .ToString();

            return filePath;
        }

        #endregion
    }
}
