using System.ComponentModel.DataAnnotations;

namespace SearchApp.Web.Models
{
    public class EngineVM
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Имя поискового движка")]
        [Required(ErrorMessage = "Не указано имя поискового движка")]
        public string Name { get; set; }
        [Display(Name = "URL запроса")]
        [Required(ErrorMessage = "Не указан URL запроса")]
        public string QueryUrl { get; set; }
        [Display(Name = "Селектор элемента результата")]
        [Required(ErrorMessage = "Не указан селектор элемента результата")]
        public string ResultElementSelector { get; set; }
        [Display(Name = "Селектор названия результата")]
        [Required(ErrorMessage = "Не указан селектор названия результата")]
        public string TitleElementSelector { get; set; }
        [Display(Name = "Селектор описания результата")]
        [Required(ErrorMessage = "Не указан селектор описания результата")]
        public string DescElementSelector { get; set; }
        [Display(Name = "Селектор ссылки результата")]
        [Required(ErrorMessage = "Не указан селектор ссылки результата")]
        public string LinkElementSelector { get; set; }
    }
}
