using Project_Interface;
using System.Linq;

namespace Project.ConsoleApp
{
    public class Parser : IFileHandling
    {
        string[] _clArguments = null;

        public Parser(string[] clArguments)
        {
            _clArguments = clArguments;
        }

        public string[] ParseFileData(string filePath)
        {
            return !string.IsNullOrEmpty(filePath) ? new string[] { filePath } : System.Array.Empty<string>();
        }

        #region  ' IFileHandling '        

        public string[] GetParsedData(string filePath) => ParseFileData(filePath).ToArray();

        #endregion
    }
}
