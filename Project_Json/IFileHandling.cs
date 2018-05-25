namespace Project_Json
{
    public interface IFileHandling
    {
        string[] ParseFileData(string filePath);

        string GetFilePath(string[] clArguments, string clArgumentNameValue);

        string[] GetParsedData(string[] clArguments);
    }
}
