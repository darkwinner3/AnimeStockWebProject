using AnimeStockWebProject.Core.Models.Picture;

namespace AnimeStockWebProject.Core.Models.Order
{
    public class UserOrderViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Status { get; set; } = null!;

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public string printType { get; set; } = null!;

        public int BookId { get; set; } 

        public int UserQuantity { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? Picture { get; set; }
    }
}
