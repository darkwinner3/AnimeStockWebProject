using AnimeStockWebProject.Areas.Admin.Models.BookType;
using System.Collections;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    internal class BookTypeViewModelComparator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            BookTypeViewModel bookType1 = (BookTypeViewModel)x;
            BookTypeViewModel bookType2 = (BookTypeViewModel)y;

            if (bookType1 == null || bookType2 == null)
            {
                return -1;
            }
            if (bookType1.Id != bookType2.Id || bookType1.Name != bookType2.Name)
            {
                return -1;
            }
            return 0;
        }
    }
}
