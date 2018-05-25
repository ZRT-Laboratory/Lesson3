namespace Project_Xml
{
    public interface IFileHandling
    {
        string[] ParseFileData(string fileName);

        string GetFilePath(string[] clArguments, string clArgumentNameValue);

        string[] GetParsedData(string[] clArguments);
    }
}
