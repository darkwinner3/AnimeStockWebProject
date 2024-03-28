using Microsoft.AspNetCore.Http;

namespace AnimeStockWebProject.Core.Models.Order
{
    public class OrderInfoViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public string PrintType { get; set; } = null!;

        public string? FilePath { get; set; }

        public string PicturePath { get; set; } = null!;


    }
}
