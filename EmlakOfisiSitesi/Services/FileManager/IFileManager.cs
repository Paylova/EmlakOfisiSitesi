namespace EmlakOfisiSitesi.Services.FileManager
{
    public interface IFileManager
    {
        string Upload(IFormFile file, string uploadDirectory);
        void Delete(string fileName, string uploadDirectory);
        void DeleteAll(List<string> fileNames, string directoryPath);
    }
}
