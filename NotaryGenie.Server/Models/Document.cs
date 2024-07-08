namespace NotaryGenie.Server.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int ClientID { get; set; }
        public required string DocumentName { get; set; }
        public DateTime UploadDate { get; set; }
        public required string FilePath { get; set; }

        public required Client Client { get; set; }
    }
}
