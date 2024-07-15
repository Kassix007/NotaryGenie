namespace NotaryGenie.Server.Models
{
    public class Notary
    {
        public int NotaryID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Client> Clients { get; set; }

        public Notary()
        {
            Clients = new List<Client>();
        }

        public Notary(int notaryID, string name, string email, ICollection<Client> clients)
        {
            NotaryID = notaryID;
            Name = name;
            Email = email;
            Clients = clients ?? new List<Client>();
        }
    }
}
