namespace NotaryGenie.Server.Models
{
    public class Notary
    {
        public int NotaryID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public required ICollection<Client> Clients { get; set; }
    }
}
