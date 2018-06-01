using System.Collections.Generic;

namespace Project_Console
{
    public interface IFileHandling
    {
        void DisplayData(List<string> parsedData);

        string GetFilePath(string clArgumentNameValue);

        string[] GetParsedData(string clArgumentNameValue);
    }
}
