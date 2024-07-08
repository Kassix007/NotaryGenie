using System.Reflection.Metadata;

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

        public Notary Notary { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<ClientDeed> ClientDeeds { get; set; }
    }
}
