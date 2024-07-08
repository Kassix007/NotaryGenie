namespace NotaryGenie.Server.Models
{
    public class ClientDeed
    {
        public int ClientID { get; set; }
        public int DeedID { get; set; }

        public required Client Client { get; set; }
        public required Deed Deed { get; set; }
    }
}
