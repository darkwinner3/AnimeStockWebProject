using AnimeStockWebProject.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AnimeStockWebProject.Common.EntityValidations.OrderEntity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; }

        public Book? Book { get; set; }

        [ForeignKey(nameof(Game))]
        public int? GameId { get; set; }

        public Game? Game { get; set; }

        public StatusEnum Status { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string? EmailAddress { get; set;}

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

    }
}
