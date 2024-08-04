namespace NotaryGenie.Server.Services.Documents
{
    public interface IDocumentService
    {
        Task<string> SaveFileAsync(IFormFile file, String name);
        string[] ExtractKeywords(string filePath);
    }
}
