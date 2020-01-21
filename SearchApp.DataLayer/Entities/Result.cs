namespace SearchApp.DataLayer.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public Request Request { get; set; }
        public int RequestId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
    }
}
