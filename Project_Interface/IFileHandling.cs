namespace Project_Interface
{
    public interface IFileHandling
    {
        string[] GetFileData(string filePath);

        string GetFilePath(string[] clArguments, string clArgumentNameValue);

        string[] GetParsedData(string[] clArguments);
    }
}
