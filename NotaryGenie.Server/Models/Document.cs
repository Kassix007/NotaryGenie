namespace NotaryGenie.Server.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int ClientID { get; set; }
        public string DocumentName { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }

        public Client Client { get; set; }

        public Document() { }

        public Document(int clientId, string documentName, DateTime uploadDate, string filePath)
        {
            ClientID = clientId;
            DocumentName = documentName;
            UploadDate = uploadDate;
            FilePath = filePath;
        }
    }
}
