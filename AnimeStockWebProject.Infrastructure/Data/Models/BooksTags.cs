using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class BooksTags
    {
        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
        
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}