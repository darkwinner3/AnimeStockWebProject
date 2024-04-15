using AnimeStockWebProject.Core.Models.Book;
using System.Collections;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    public class BookViewModelComparator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            BookViewModel book1 = (BookViewModel)x;
            BookViewModel book2 = (BookViewModel)y;

            if (book1 == null || book2 == null)
            {
                return -1;
            }
            if (book1.Id != book2.Id || book1.Title != book2.Title || book1.Price != book2.Price || book1.Author != book2.Author
                || book1.Illustrator != book2.Illustrator || book1.BookType != book2.BookType || book1.ReleaseDate != book2.ReleaseDate
                || book1.Description != book2.Description)
            {
                return -1;
            }
            return 0;
        }
    }
}
