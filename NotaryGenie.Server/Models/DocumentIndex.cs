namespace NotaryGenie.Server.Models
{
    public class DocumentIndex
    {
        public int DocumentID { get; set; }
        public string Keyword { get; set; }
        public string Location { get; set; }
        public Document Document { get; set; }
    }
}
