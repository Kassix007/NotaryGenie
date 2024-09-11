using Google.Cloud.DocumentAI.V1;
using Google.Protobuf;
using NotaryGenie.Server.Models;

namespace NotaryGenie.Server.Services.OCRProcessors
{
    public interface IIdCardFrontService
    {
        Task<List<DocumentEntity>> ProcessDocumentAsync(string filePath);
    }

    public class IdCardFrontService : IIdCardFrontService
    {
        private readonly string _projectId;
        private readonly string _location;
        private readonly string _processorId;

        public IdCardFrontService(IConfiguration configuration)
        {
            _projectId = configuration["GoogleCloud:ProjectId"];
            _location = configuration["GoogleCloud:Location"];
            _processorId = configuration["GoogleCloud:ProcessorId"];
        }

        public async Task<List<DocumentEntity>> ProcessDocumentAsync(string filePath)
        {
            // Dynamically determine MIME type
            string mimeType = GetMimeType(filePath);

            // Initialize client
            var client = new DocumentProcessorServiceClientBuilder
            {
                Endpoint = $"{_location}-documentai.googleapis.com"
            }.Build();

            string name = ProcessorName.FromProjectLocationProcessor(_projectId, _location, _processorId).ToString();

            // Load the file into memory
            var imageContent = await File.ReadAllBytesAsync(filePath);

            // Create the request
            var rawDocument = new RawDocument
            {
                Content = ByteString.CopyFrom(imageContent),
                MimeType = mimeType
            };

            var processRequest = new ProcessRequest
            {
                Name = name,
                RawDocument = rawDocument,
                ProcessOptions = new ProcessOptions
                {
                    IndividualPageSelector = new ProcessOptions.Types.IndividualPageSelector
                    {
                        Pages = { 1 }
                    }
                }
            };

            var result = await client.ProcessDocumentAsync(processRequest);
            var document = result.Document;

            // Extract entities
            var entities = new List<DocumentEntity>();
            foreach (var entity in document.Entities)
            {
                entities.Add(new DocumentEntity
                {
                    Type = entity.Type,
                    MentionText = entity.MentionText,
                    Confidence = entity.Confidence
                });
            }

            return entities;
        }

        private string GetMimeType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".tiff" => "image/tiff",
                ".tif" => "image/tiff",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => throw new ArgumentException($"Unsupported file type: {extension}")
            };
        }
    }
}
