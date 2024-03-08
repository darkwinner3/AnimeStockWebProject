using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Common
{
    public static class GeneralAplicaitonConstants
    {
        //Pager page size
        public const int DefaultPageSize = 5;
        //Roles
        public const string AdminRoleName = "Administrator";
        public const string AdminAreaName = "Admin";
        public const string ModeratorRoleName = "Moderator";
        public const string UserRoleName = "User";

        //Cache keys and durations
        public const string AdminUsersCacheKey = "AllUsers";
        public const int AdminUsersDuration = 2;
        public const string UserInfoCacheKey = "UserInfoCacheKey-{0}";
        public const int UserInfoCacheDuration = 5;
        public const string BookTagsCacheKey = "BookTagsCacheKey";
        public const int BookTagsCacheDuration = 5;
    }
}
