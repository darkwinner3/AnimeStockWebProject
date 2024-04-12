using AnimeStockWebProject.Areas.Admin.Models.User;
using System.Collections;

namespace AnimeStockWebProject.Services.Tests.Comparators
{
    public class UsersViewModelComperator : IComparer
    {
        public int Compare(object? x, object? y)
        {
            UsersViewModel user1 = (UsersViewModel)x;
            UsersViewModel user2 = (UsersViewModel)y;

            if (user1 == null || user2 == null)
            {
                return -1;
            }
            if (user1.Id != user2.Id || user1.UserName != user2.UserName || user1.Email != user2.Email)
            {
                return -1;
            }
            return 0;
        }
    }
}
