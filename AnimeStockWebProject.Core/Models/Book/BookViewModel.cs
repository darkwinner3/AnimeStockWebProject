using AnimeStockWebProject.Infrastructure.Data.Enums;
using AnimeStockWebProject.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnimeStockWebProject.Core.Models.Book
{
    using Book;
    public class BookViewModel
    {
        public BookViewModel()
        {
            IsFavorite = false;
        }
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Illustrator { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string BookType { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public string? PrintType { get; set; }

        public decimal Price { get; set; }

        public string PicturePath { get; set; } = null!;

        public bool IsFavorite { get; set; }
    }
}
