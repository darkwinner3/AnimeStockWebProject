using AnimeStockWebProject.Areas.Admin.Models.BookType;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.Picture;
using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.BookEntity;

namespace AnimeStockWebProject.Areas.Admin.Models.Book
{
    public class BookAddViewModel
    {
        public BookAddViewModel()
        {
            BookTags = new List<TagViewModel>();
            PicturesPaths = new List<string>();
            SelectedBookTagIds = new List<int>();
            BookTypes = new List<BookTypeViewModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(AuthorMaxLength, MinimumLength = AuthorMinLength)]
        public string Author { get; set; } = string.Empty;
        [Required]
        [StringLength(IllustratorMaxLength, MinimumLength = IllustratorMinLength)]
        public string Illustrator { get; set; } = string.Empty;
        [Required]
        [StringLength(PublisherMaxLength, MinimumLength = PublisherMinLength)]
        public string Publisher { get; set; } = string.Empty;
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;
        public int BookTypeId { get; set; }

        public IEnumerable<BookTypeViewModel> BookTypes { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string PrintType { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public byte[]? PdfContent { get; set; }
        [Required]
        public string? FilePath { get; set; }

        public IEnumerable<TagViewModel> BookTags { get; set; }


        public List<int> SelectedBookTagIds { get; set; }

        public IFormFileCollection Pictures { get; set; }

        public List<string> PicturesPaths { get; set; }
    }
}
