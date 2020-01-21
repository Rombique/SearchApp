using System;
using System.Collections.Generic;

namespace SearchApp.DataLayer.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public string Words { get; set; }
        public DateTime DateCreated { get; set; }
        public Engine Engine { get; set; }
        public int EngineId { get; set; }
        public virtual ICollection<Result> SearchResults { get; set; } = new List<Result>();
    }
}
