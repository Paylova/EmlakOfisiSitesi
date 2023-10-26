using SixLabors.ImageSharp.Formats.Png;

namespace EmlakOfisiSitesi.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public string Upload(IFormFile file, string uploadDirectory)
        {
            if (file == null || file.Length == 0)
                return null;

            string uniqueFileName = GetUniqueFileName(file.FileName);
            string filePath = Path.Combine(uploadDirectory, uniqueFileName);

            if (!Directory.Exists(uploadDirectory))
                Directory.CreateDirectory(uploadDirectory);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var image = Image.Load(filePath);
            image.Mutate(x => x.Resize(new ResizeOptions { Size = new Size(1000, 1000), Mode = ResizeMode.Crop }));
            image.Mutate(x => x.BackgroundColor(Color.Transparent));
            image.Save(filePath, new PngEncoder());
            return uniqueFileName;
        }

        public void Delete(string fileName, string uploadDirectory)
        {
            string filePath = Path.Combine(uploadDirectory, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private string GetUniqueFileName(string fileName)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            return uniqueFileName;
        }

        public void DeleteAll(List<string> fileNames, string directoryPath)
        {
            foreach (string fileName in fileNames)
            {
                string filePath = Path.Combine(directoryPath, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }
    }
}
