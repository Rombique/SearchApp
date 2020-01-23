namespace SearchApp.BusinessLayer.DTO
{
    public class EngineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QueryUrl { get; set; }
        public string ResultElementSelector { get; set; }
        public string TitleElementSelector { get; set; }
        public string DescElementSelector { get; set; }
        public string LinkElementSelector { get; set; }
    }
}
