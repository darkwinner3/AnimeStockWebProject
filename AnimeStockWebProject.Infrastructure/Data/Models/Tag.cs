using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.TagEntity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            BookTags = new List<BooksTags>();
        }
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<BooksTags> BookTags { get; set; }
    }
}