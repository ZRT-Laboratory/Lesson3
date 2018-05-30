using System.Collections.Generic;

namespace Project_Interface
{
    public interface IFileHandling
    {
        void DisplayData(List<string> dataItems);

        string GetFilePath(string[] clArguments, string clArgumentNameValue);
    }
}
