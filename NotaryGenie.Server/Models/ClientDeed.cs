namespace NotaryGenie.Server.Models
{
    public class ClientDeed
    {
        public int ClientID { get; set; }
        public int DeedID { get; set; }

        public required Client Client { get; set; }
        public required Deed Deed { get; set; }

        public ClientDeed()
        {
        }

        public ClientDeed(int clientID, int deedID, Client client, Deed deed)
        {
            ClientID = clientID;
            DeedID = deedID;
            Client = client;
            Deed = deed;
        }
    }
}
