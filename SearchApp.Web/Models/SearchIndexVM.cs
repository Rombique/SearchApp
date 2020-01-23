using System.ComponentModel.DataAnnotations;

namespace SearchApp.Web.Models
{
    public class SearchIndexVM
    {
        [Required(ErrorMessage = "Не указаны ключевые слова")]
        public string Words { get; set; }
        [Display(Name = "Искать в БД")]
        public bool IsOfflineSearch { get; set; }
    }
}
