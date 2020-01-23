using System;
using System.Collections.Generic;

namespace SearchApp.BusinessLayer.DTO
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string Words { get; set; }
        public DateTime DateCreated { get; set; }
        public int EngineId { get; set; }
        public EngineDTO Engine { get; set; }
        public ICollection<ResultDTO> Results { get; set; }
    }
}
