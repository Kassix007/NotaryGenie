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

    public class DocumentEntity
    {
        public string Type { get; set; }
        public string MentionText { get; set; }
        public double Confidence { get; set; }
    }

    public class ProofOfAddress : Document
    {
        public string Address { get; set; }
        public string TypeOfProof { get; set; }    // e.g., Utility Bill, Bank Statement
        public string NameOnProof { get; set; }    // Name mentioned on the proof of address document

        public ProofOfAddress() { }

        public ProofOfAddress(int documentId, int clientId, string documentName, DateTime uploadDate, string? filePath,
                              string address, string typeOfProof, string nameOnProof)
            : base((int)clientId, documentName, uploadDate, filePath)
        {
            Address = address;
            TypeOfProof = typeOfProof;
            NameOnProof = nameOnProof;
        }
    }
}
