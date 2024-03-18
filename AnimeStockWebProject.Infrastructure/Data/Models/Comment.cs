using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.CommentEntity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
       
        public int? BookId { get; set; }
        public Book? Book { get; set; } 
       
        public int? GameId { get; set; }
        public Game? Game { get; set; } 
        public string UserName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
