﻿using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.Comment;
using AnimeStockWebProject.Core.Models.Picture;

namespace AnimeStockWebProject.Core.Models.Book
{
    using Pager;
    using System.ComponentModel.DataAnnotations;

    public class BookInfoViewModel
    {
        public BookInfoViewModel()
        {
            BookTags = new List<TagViewModel>();
            Comments = new List<CommentViewModel>();
            Pictures = new List<PictureViewModel>();
            IsFavorite = false;
        }

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Illustrator { get; set; } = string.Empty;

        public string Publisher { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string BookType { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public int Pages { get; set; }

        public int BookQuantity { get; set; }

        public int UserQuantity { get; set; }

        public string PrintType { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsFavorite { get; set; }

        public byte[] PdfContent { get; set; }

        public string? FilePath { get; set; }

        public IEnumerable<TagViewModel> BookTags { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public Pager CommentsPager { get; set; } = null!;

        public IEnumerable<PictureViewModel> Pictures { get; set; }

        public ValidationResult Validate(ValidationContext validationContext)
        {
            if (UserQuantity > BookQuantity)
            {
                return new ValidationResult("Order quantity exceeded book quantity");
            }

            return null;
        }
    }
}
