namespace AnimeStockWebProject.Common
{
    public static class GeneralAplicaitonConstants
    {
        //Pager page size
        public const int AdminBooksPageSize = 6;
        public const int CommentsPageSize = 10;
        public const int BooksPageSize = 5;

        //book pages
        public const int BookPages = 30;
        //Roles
        public const string AdminRoleName = "Administrator";
        public const string AdminAreaName = "Admin";
        public const string ModeratorRoleName = "Moderator";
        public const string UserRoleName = "User";

        //Cache keys and durations
        public const string HomePageCacheKey = "HomeCache";
        public const int HomePageCacheDuration = 5;
        public const string AdminUsersCacheKey = "AllUsers";
        public const int AdminUsersDuration = 2;
        public const string AdminDashBoardCacheKey = "AdminDashBoardKey";
        public const int AdminDashBoardCacheDuration = 5;
        public const string UserInfoCacheKey = "UserInfoCacheKey-{0}";
        public const int UserInfoCacheDuration = 5;
        public const string BookTagsCacheKey = "BookTagsCacheKey";
        public const int BookTagsCacheDuration = 5;
        public const string BookTypesCacheKey = "BookTypesCacheKey";
        public const int BookTypesCacheDuration = 5;
        public const string UserFavoriteItemsCacheKey = "UserFavoriteCacheKey-{0}";
        public const int UserFavoriteItemsCacheDuration = 5;
        public const string UserOrdersCacheKey = "UserOrderCacheKey-{0}";
        public const int UserOrdersCacheDuration = 5;
        public const string BookTagAdminCacheKey = "BookAdminCacheKey";
        public const int BookTagAdminCacheDuration = 5;


    }
}
