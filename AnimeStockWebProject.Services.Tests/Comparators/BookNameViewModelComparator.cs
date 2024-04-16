using AnimeStockWebProject.Core.Models.Book;
using System.Collections;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    public class BookNameViewModelComparator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            BookNameViewModel book1 = (BookNameViewModel)x;
            BookNameViewModel book2 = (BookNameViewModel)y;

            if (book1 == null || book2 == null)
            {
                return -1;
            }
            if (book1.Id != book2.Id || book1.Title != book2.Title || book1.Price != book2.Price)
            {
                return -1;
            }
            return 0;
        }
    }
}
