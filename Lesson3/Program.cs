using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Project_Interface;

namespace Project_Console
{
    public class Program
    {
        public static void Main(string[] clArguments)
        {
            if (clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0))
            {
                    IFileHandling ifh = new Parser(clArguments);

                    //create a list then order it so nulls are last in the list
                    List<string> parsedData = ifh.GetParsedData(GetFilePath(clArguments, "-json"))
                        .Concat(ifh.GetParsedData(GetFilePath(clArguments, "-xml")))
                        .OrderBy(fh => fh)
                        .ToList()
                        .OrderBy(ai => ai == null)
                        .ToList();

                    parsedData.ForEach(vi => Console.WriteLine("{0}", vi));
            }
            else
            {
                throw new ArgumentException("Invalid arguments.");
            }

            string GetFilePath(string[] arguments, string argumentNameValue)
            {
                string filePath = clArguments.SkipWhile(a => string.Compare(a, argumentNameValue, true) != 0)
                    .Skip(1)
                    .DefaultIfEmpty("")
                    .First()
                    .ToString();

                return filePath;
            }
        }      
    }
}
