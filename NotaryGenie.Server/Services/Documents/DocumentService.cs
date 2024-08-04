namespace NotaryGenie.Server.Services.Documents
{
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;

    public class DocumentService : IDocumentService
    {
        public async Task<string> SaveFileAsync(IFormFile file, String name)
        {
            var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "DocumentStorage");
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            var filePath = Path.Combine(storagePath, name);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public string[] ExtractKeywords(string filePath)
        {          
            return new string[] { "keyword1", "keyword2" };
        }

        public Task<string> SaveFileAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }

}
