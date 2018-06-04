using Project_Interface;
using System.Linq;

namespace Project_Console
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
            return new string[] { filePath };
        }

        #region  ' IFileHandling '        

        public string[] GetParsedData(string filePath) => ParseFileData(filePath).ToArray();

        #endregion
    }
}
