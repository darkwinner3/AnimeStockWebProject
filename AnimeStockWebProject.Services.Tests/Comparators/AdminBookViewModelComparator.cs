using AnimeStockWebProject.Areas.Admin.Models.Book;
using System.Collections;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    public class AdminBookViewModelComparator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            BookViewModel book1 = (BookViewModel)x;
            BookViewModel book2 = (BookViewModel)y;

            if (book1 == null || book2 == null)
            {
                return -1;
            }
            if (book1.Id != book2.Id || book1.Title != book2.Title || book1.PictureUrl != book2.PictureUrl || book1.BookTypeId != book2.BookTypeId)
            {
                return -1;
            }
            return 0;
        }
    }
}
