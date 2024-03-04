using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Common
{
    public static class EntityValidations
    {
        public static class UserEntity
        {
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 35;

            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 30;

            public const int FirstNameMinValue = 3;
            public const int FirstNameMaxValue = 15;

            public const int LastNameMinValue = 4;
            public const int LastNameMaxValue = 15;

            public const int EmailAddressMinValue = 10;
            public const int EmailAddressMaxValue = 30;

            public const int PhoneNumberMaxLength = 20;
        }

        public static class BookEntity
        {
            public const int TitleMaxLength = 60;
            public const int TitleMinLength = 1;

            public const int AuthorMaxLength = 30;
            public const int AuthorMinLength = 2;

            public const int IllustratorMaxLength = 30;
            public const int IllustratorMinLength = 2;

            public const int PublisherMaxLength = 200;
            public const int PublisherMinLength = 2;

            public const int DescriptionMaxLength = 2000;
            public const int DescriptionMinLength = 0;

            public const string MinPrice = "0.00";
            public const string MaxPrice = "1000.00";

            public const int MaxPages = 10000;
            public const int MinPages = 1;

            public const string ReleaseDate = "";
        }

        public static class GameEntity
        {
            public const int NameMaxLength = 60;
            public const int NameMinLength = 1;

            public const int DeveloperMaxLength = 1000;
            public const int DeveloperMinLength = 2;

            public const int PublisherMaxLength = 40;
            public const int PublisherMinLength = 2;

            public const int DescriptionMaxLength = 2000;
            public const int DescriptionMinLength = 0;

            public const string MinPrice = "0.00";
            public const string MaxPrice = "1000.00";

            public const string ReleaseDate = "";
        }

        public static class BookTypeEntity
        {
            public const int NameMaxLength = 15;
            public const int NameMinLength = 4;
        }

        public static class CommentEntity
        {
            public const int DescriptionMaxLength = 50;
            public const int DescriptionMinLength = 1;
        }

        public static class OrderEntity
        {
            public const int FirstNameMaxLength = 15;
            public const int FirstNameMinLength = 3;

            public const int EmailMaxLength = 30;
            public const int EmailMinLength = 2;
        }

        public static class GenreEntity
        {
            public const int NameMaxLength = 35;
            public const int NameMinLength = 3;
        }
        public static class TagEntity
        {
            public const int NameMaxLength = 15;
            public const int NameMinLength = 3;
        }
    }
}
