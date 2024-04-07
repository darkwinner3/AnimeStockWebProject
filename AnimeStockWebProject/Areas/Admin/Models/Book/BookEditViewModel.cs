using AnimeStockWebProject.Areas.Admin.Models.BookType;
using AnimeStockWebProject.Areas.Admin.Models.Pictures;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.Picture;
using AnimeStockWebProject.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.BookEntity;

namespace AnimeStockWebProject.Areas.Admin.Models.Book
{
    public class BookEditViewModel
    {
        public BookEditViewModel()
        {
            BookTags = new List<TagViewModel>();
            PicturesPaths = new List<string>();
            SelectedBookTagIds = new List<int>();
            BookTypes = new List<BookTypeViewModel>();
        }
        public int Id { get; init; }

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

        public IEnumerable<BookTypeViewModel> BookTypes { get; set; }
        public int BookTypeId { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int Pages { get; set; }
        public int Quantity { get; set; }
        [Required]
        public PrintTypeEnum PrintType { get; set; }
        [Required]
        public decimal Price { get; set; }
        public IFormFile? BookFile { get; set; }
        public string? FilePath { get; set; }

        public IEnumerable<TagViewModel> BookTags { get; set; }

        public List<int> SelectedBookTagIds { get; set; }

        public IFormFile? NewCoverImg { get; set; }

        public IFormFileCollection? NewPictures { get; set; }
        public PictureAdminViewModel? CoverImg { get; set; }
        public IEnumerable<PictureAdminViewModel> Pictures { get; set; }

        public IEnumerable<TagViewModel>? currentTags { get; set; }

        public List<string> PicturesPaths { get; set; }
    }
}
