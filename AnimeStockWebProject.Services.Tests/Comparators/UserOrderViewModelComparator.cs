using AnimeStockWebProject.Core.Models.Order;
using System.Collections;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    public class UserOrderViewModelComparator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            UserOrderViewModel userOrder1 = (UserOrderViewModel)x;
            UserOrderViewModel userOrder2 = (UserOrderViewModel)y;

            if (userOrder1 == null || userOrder2 == null)
            {
                return -1;
            }
            if (userOrder1.Id != userOrder2.Id || userOrder1.OrderDate != userOrder2.OrderDate || userOrder1.Price != userOrder2.Price 
                || userOrder1.Status != userOrder2.Status || userOrder1.UserQuantity != userOrder2.UserQuantity)
            {
                return -1;
            }
            return 0;
        }
    }
}
