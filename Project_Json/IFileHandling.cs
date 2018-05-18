namespace Project_Json
{
    interface IFileHandling
    {
        string GetFilePathFromArgument(string[] clArguments, string clArgumentNameValue);

        string GetFileNameFromDialog(string filePath, string title);

        string[] GetValuesFromFile(string fileName);
    }
}
