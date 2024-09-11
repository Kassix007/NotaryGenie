using NotaryGenie.Server.Models;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace NotaryGenie.Server.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public int NotaryID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }

        [JsonIgnore]
        public Notary Notary { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<ClientDeed> ClientDeeds { get; set; }
    }
}

namespace NotaryGenie.Server.Dtos
{
    public class ClientDto
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }
        public List<DocumentDto> Documents { get; set; } = new();
        public List<ClientDeedDto> ClientDeeds { get; set; } = new();
    }

    public class DocumentDto
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }
    }

    public class ClientDeedDto
    {
        public int DeedID { get; set; }

    }
}

