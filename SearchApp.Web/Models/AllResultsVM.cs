using System.Collections.Generic;

namespace SearchApp.Web.Models
{
    public class AllResultsVM
    {
        public string Error { get; set; }
        public IEnumerable<ResultVM> Results { get; set; }
    }
}
