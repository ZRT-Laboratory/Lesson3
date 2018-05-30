using System.Collections.Generic;

namespace Project_Console
{
    public interface IFileHandling
    {
        void DisplayData(List<string> valueItems);

        string GetFilePath(string[] clArguments, string clArgumentNameValue);
    }
}
