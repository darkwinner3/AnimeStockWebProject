using AnimeStockWebProject.Core.Models.Order;
using System.Collections;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    public class OrderInfoViewModelComperator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            OrderInfoViewModel order1 = (OrderInfoViewModel)x;
            OrderInfoViewModel order2 = (OrderInfoViewModel)y;

            if (order1 == null || order2 == null)
            {
                return -1;
            }
            if (order1.Id != order2.Id || order1.Title != order2.Title || order1.Price != order2.Price || order1.Email != order2.Email
                || order1.PrintType != order2.PrintType || order1.FilePath != order2.FilePath || order1.FirstName != order2.FirstName
                || order1.PicturePath != order2.PicturePath)
            {
                return -1;
            }
            return 0;
        }
    }
}
