using System.ComponentModel.DataAnnotations;

namespace SearchApp.Web.Models
{
    public class DeleteEngineVM
    {
        [Display(Name = "Id поискового движка")]
        [Required(ErrorMessage = "Не указан Id поискового движка")]
        public int Id { get; set; }
    }
}
