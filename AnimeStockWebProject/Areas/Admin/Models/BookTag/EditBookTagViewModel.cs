using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.TagEntity;
namespace AnimeStockWebProject.Areas.Admin.Models.BookTag
{
    public class EditBookTagViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
