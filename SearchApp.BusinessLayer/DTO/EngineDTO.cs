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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            if (!(obj is EngineDTO))
                return false;
            EngineDTO engine = obj as EngineDTO;
            return Id == engine.Id
                && Name == engine.Name
                && QueryUrl == engine.QueryUrl
                && ResultElementSelector == engine.ResultElementSelector
                && TitleElementSelector == engine.TitleElementSelector
                && DescElementSelector == engine.DescElementSelector
                && LinkElementSelector == engine.LinkElementSelector;
                   
        }

        public override int GetHashCode()
        {
            int idHashCode = Id.GetHashCode();
            int nameHashCode = Name.GetHashCode();
            int urlHashCode = QueryUrl.GetHashCode();
            int resultESHashCode = ResultElementSelector.GetHashCode();
            int titleESHashCode = TitleElementSelector.GetHashCode();
            int descESHashCode = DescElementSelector.GetHashCode();
            int linkESHashCode = LinkElementSelector.GetHashCode();
            return idHashCode ^ nameHashCode ^ urlHashCode ^ resultESHashCode ^ titleESHashCode ^ descESHashCode ^ linkESHashCode;
        }
    }
}
