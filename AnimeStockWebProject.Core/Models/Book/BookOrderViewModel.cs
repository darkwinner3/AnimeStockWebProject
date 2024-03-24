using AnimeStockWebProject.Core.Models.Picture;

namespace AnimeStockWebProject.Core.Models.Book
{
    public class BookOrderViewModel
    {
        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public int BookId { get; set; }

        public int UserQuantity { get; set; }

        public DateTime ReleaseDate { get; set; }

        public PictureViewModel? Picture { get; set; }
    }
}
