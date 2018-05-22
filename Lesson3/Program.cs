using System;
using System.IO;
using System.Linq;

namespace Lesson3
{
    class Program
    {
        static void Main(string[] clArguments)
        {
            bool validArguments = clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0);
            bool validJsonPath = ValidFilePath(clArguments, "-json");
            bool validXmlPath = ValidFilePath(clArguments, "-xml");

            if (validArguments && (validJsonPath || validXmlPath))
            {
                Console.WriteLine("Valid arguments.");
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }

            bool ValidFilePath(string[] arguments, string clNameValue)
            {
                string filePath = clArguments.SkipWhile(a => string.Compare(a, clNameValue, true) != 0)
                    .Skip(1)
                    .DefaultIfEmpty("")
                    .First()
                    .ToString();

                return Path.IsPathRooted(filePath);
            }
        }
    }
}
