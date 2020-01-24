using SearchApp.BusinessLayer.DTO;
using System.Collections.Generic;

namespace SearchApp.BusinessLayer.Infrastructure
{
    public class SearchResult
    {
        public SearchResult()
        {
        }

        public SearchResult(bool succeedeed, string message)
        {
            Succeedeed = succeedeed;
            Message = message;
        }

        public string EngineName { get; set; }
        public int EngineId { get; set; }
        public bool Succeedeed { get; set; }
        public string Message { get; set; }
        public IEnumerable<ResultDTO> Results { get; set; }
    }
}
