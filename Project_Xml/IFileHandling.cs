namespace Project_Xml
{
    public interface IFileHandling
    {
        string[] GetFileData(string fileName);

        string GetFilePath(string[] clArguments, string clArgumentNameValue);

        string[] GetParsedData(string[] clArguments);
    }
}
