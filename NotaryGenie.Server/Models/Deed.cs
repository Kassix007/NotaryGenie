namespace NotaryGenie.Server.Models
{
    public class Deed
    {
        public int DeedID { get; set; }
        public required string DeedName { get; set; }
        public DateTime DeedDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public required string TVNo { get; set; }
        public required string DeedDetails { get; set; }

        public required ICollection<ClientDeed> ClientDeeds { get; set; }
    }
}
