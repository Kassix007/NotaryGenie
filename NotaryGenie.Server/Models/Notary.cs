namespace NotaryGenie.Server.Models
{
    public class Notary
    {
        public int NotaryID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Hashed password

        public ICollection<Client> Clients { get; set; }

        // Default constructor
        public Notary()
        {
            Clients = new List<Client>();
        }

        // Parameterized constructor
        public Notary(int notaryID, string name, string email, string password, ICollection<Client> clients)
        {
            NotaryID = notaryID;
            Name = name;
            Email = email;
            Password = password;
            Clients = clients ?? new List<Client>();
        }
    }
}
