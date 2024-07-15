using System;
using System.Collections.Generic;

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

        public Deed()
        {
            ClientDeeds = new List<ClientDeed>();
        }

        public Deed(int deedID, string deedName, DateTime deedDate, DateTime registrationDate, string tvNo, string deedDetails, ICollection<ClientDeed> clientDeeds)
        {
            DeedID = deedID;
            DeedName = deedName;
            DeedDate = deedDate;
            RegistrationDate = registrationDate;
            TVNo = tvNo;
            DeedDetails = deedDetails;
            ClientDeeds = clientDeeds ?? new List<ClientDeed>();
        }
    }
}
