using AnimeStockWebProject.Infrastructure.Data.Enums;
using AnimeStockWebProject.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AnimeStockWebProject.Core.Models.Comment;
using AnimeStockWebProject.Core.Models.Picture;

namespace AnimeStockWebProject.Core.Models.Book
{
    public class BookInfoViewModel
    {
        public BookInfoViewModel()
        {
            BookTags = new List<BooksTags>();
            Comments = new List<CommentViewModel>();
            Pictures = new List<PictureViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Illustrator { get; set; } = string.Empty;

        public string Publisher { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string BookType { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public int Pages { get; set; }

        public int Quantity { get; set; }

        public string PrintType { get; set; } = null!;

        public decimal Price { get; set; }

        public List<BooksTags> BookTags { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<PictureViewModel> Pictures { get; set; }
    }
}
