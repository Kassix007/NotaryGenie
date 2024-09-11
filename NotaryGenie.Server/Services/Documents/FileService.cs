using FileInfo = NotaryGenie.Server.Models.FileInfo;

namespace NotaryGenie.Server.Services.Documents
{
    public interface IFileService
    {
        List<FileInfo> GetFilesFromTempStorage();
    }

    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly string _tempStoragePath;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
            _tempStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "TempDocumentStorage");
        }

        public List<FileInfo> GetFilesFromTempStorage()
        {
            List<FileInfo> fileInfos = new List<FileInfo>();

            try
            {
                if (Directory.Exists(_tempStoragePath))
                {
                    var filePaths = Directory.GetFiles(_tempStoragePath, "*", SearchOption.AllDirectories);
                    fileInfos = filePaths.Select(filePath => new FileInfo
                    {
                        FileName = Path.GetFileName(filePath),
                        FilePath = filePath
                    }).ToList();

                    _logger.LogInformation($"Found {fileInfos.Count} files in TempDocumentStorage.");
                }
                else
                {
                    _logger.LogWarning("The TempDocumentStorage directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving files from TempDocumentStorage.");
            }

            return fileInfos;
        }


    }
}

