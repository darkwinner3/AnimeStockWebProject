namespace AnimeStockWebProject.Services.Tests
{
    using AnimeStockWebProject.Common;
    using AnimeStockWebProject.Infrastructure.Data;
    using Infrastructure.Data.Enums;
    using Infrastructure.Data.Models;
    using System.Globalization;

    public static class DatabaseSeeder
    {
        public static Book book1;
        public static Book book2;
        public static Book book3;

        public static Comment comment1;
        public static Comment comment2;
        public static Comment comment3;

        public static Tag tag1;
        public static Tag tag2;
        public static Tag tag3;

        public static BookType bookType1;
        public static BookType bookType2;

        public static void SeedDatabase(AnimeStockDbContext animeStockDbContext)
        {
            animeStockDbContext.Books.AddRange(SeedBooks());
            animeStockDbContext.Comments.AddRange(SeedComments());
            animeStockDbContext.Tags.AddRange(SeedTags());
            animeStockDbContext.BookTypes.AddRange(SeedBookTypes());
            animeStockDbContext.FavoriteProducts.AddRange(SeedFavoriteProducts());
            animeStockDbContext.Pictures.AddRange(new Picture() 
            {
                Id = 1, Path = "test/path", BookId = 2, IsDeleted = false
            },
            new Picture()
            {
                Id = 2, Path = "/cover/test/path", BookId = 2, IsDeleted = false
            },
            new Picture()
            {
                Id = 3,
                Path = "/cover/test/path",
                BookId = 1,
                IsDeleted = false
            },
            new Picture()
            {
                Id = 4,
                Path = "/cover/test/path",
                BookId = 3,
                IsDeleted = false
            });
            animeStockDbContext.SaveChanges();
        }

        private static IEnumerable<Book> SeedBooks()
        {
            List<Book> books = new List<Book>();


            book1 = new Book()
            {
                Id = 1,
                Title = "Date A Live, Vol. 1",
                Author = "Koushi Tachibana",
                Illustrator = "Tsunako",
                Publisher = "Yen On",
                Description = "April 10. The first day of school. Shido Itsuka is rudely awoken by his personal alarm clock—his little sister. It’s shaping up to be another typical day…well, as typical as it gets on a planet plagued by massive spatial quakes. Little does Shido know, however, his life is about to take a sudden turn when he encounters the source of this destructive phenomenon—a girl his age, apparently known as a Spirit. Turns out, there are only two ways he can stop her from unleashing hell on the world: Eliminate her by force or placate her…by taking her out on a date and making her fall in love with him!",
                BookTypeId = 1,
                ReleaseDate = DateTime.ParseExact("March 23, 2021", EntityValidations.BookEntity.ReleaseDate, CultureInfo.InvariantCulture),
                Pages = 176,
                Quantity = 7,
                PrintType = PrintTypeEnum.Phisycal,
                Price = 12.23m,
                FilePath = "/Books/Date A Live/Date A Live, Vol. 1_ Dead-End Tohka.pdf",
                BookTags = new List<BooksTags>()
                {
                    new BooksTags() {TagId = 1, BookId = 1},
                    new BooksTags() { TagId = 3, BookId = 1},
                    new BooksTags() {TagId = 2, BookId = 1},
                },
                Orders = new List<Order>()
                {
                    new Order()
                    {
                        Id = Guid.Parse("80833707-38a8-4b59-bfc1-32e3b4b16048"),
                        GameId = null,
                        Status = StatusEnum.Delivered,
                        OrderDate = DateTime.Parse("2024-04-11"),
                        UserOrders = 2,
                        FirstName = "John",
                        EmailAddress = "john@gmail.com",
                        UserId = Guid.Parse("34f9e2fe-0165-4892-870d-bc7bf5f5852c") ,
                        TotalPrice = 12.23m * 2
                    },
                    new Order()
                    {
                        Id = Guid.Parse("8067fcf1-78f9-40e5-84e0-7170e462fd40"),
                        GameId = null,
                        Status = StatusEnum.Delivered,
                        OrderDate = DateTime.Parse("2024-05-15"),
                        UserOrders = 5,
                        FirstName = "Georgi",
                        EmailAddress = "Georgi@gmail.com",
                        UserId = Guid.Parse("34f9e2fe-0165-4892-870d-bc7bf5f5852c"),
                        TotalPrice = 12.23m * 5
                    }
                }
            };
            book2 = new Book()
            {
                Id = 2,
                Title = "Date A Live, Vol. 2",
                Author = "Koushi Tachibana",
                Illustrator = "Tsunako",
                Publisher = "Yen On",
                Description = "Things have only gotten stranger for Shido ever since Tohka transferred to his school. These days, his life teeters between heaven and hell, and the forecast today points toward the latter. He’s already been caught in the line of fire between Tohka and Origami, and a sudden downpour leaves him sopping wet. Just his luck. Lately, it seems the only place he can catch a break is in the comfort of his own home. That’s all about to change, however, when a new “training” regimen calls for him to live with Tohka—and while Shido is at his most vulnerable, a second Spirit emerges from the storm…",
                BookTypeId = 1,
                ReleaseDate = DateTime.ParseExact("May 25, 2021", EntityValidations.BookEntity.ReleaseDate, CultureInfo.InvariantCulture),
                Pages = 184,
                Quantity = 35,
                PrintType = PrintTypeEnum.Phisycal,
                Price = 11.34m,
                FilePath = "/Books/Date A Live/Date A Live, Vol. 2_ Puppet Yoshino.pdf",
                BookTags = new List<BooksTags>()
                {
                    new BooksTags() {TagId = 1, BookId = 2},
                    new BooksTags() { TagId = 3, BookId = 2},
                },
                Orders = new List<Order>()
                {
                    new Order()
                    {
                        Id = Guid.Parse("0f8e1ca6-b0aa-47f6-8284-666650c464d0"),
                        GameId = null,
                        Status = StatusEnum.Ordered,
                        OrderDate = DateTime.Parse("2024-06-21"),
                        UserOrders = 4,
                        FirstName = "Alice",
                        EmailAddress = "alice@gmail.com",
                        UserId = Guid.Parse("61907c5c-0a28-4b74-8f65-9300965f2863"),
                        TotalPrice = 11.34m * 4
                        
                    },
                    new Order()
                    {
                        Id = Guid.Parse("000488c2-02a4-4cdc-8b51-8925142b3380"),
                        GameId = null,
                        Status = StatusEnum.Ordered,
                        OrderDate = DateTime.Parse("2024-05-25"),
                        UserOrders = 2,
                        FirstName = "Bob",
                        EmailAddress = "bob@gmail.com",
                        UserId = Guid.Parse("a6b56a77-bc52-421d-9c2b-dc76decdf570"),
                        TotalPrice = 11.34m * 2
                    },
                }
            };
            book3 = new Book()
            {
                Id = 3,
                Title = "Seirei Gensouki: Spirit Chronicles Volume 23",
                Author = "Yuri Kitayama",
                Illustrator = "Riv",
                Publisher = "J-Novel Club",
                Description = "Rio and Sora journey to the site of Saint Erica’s summoning to find clues about the transcendent rules. As Rio walks in the Saint’s footsteps, more questions arise about the power that the heroes wield.\r\n\r\nMeanwhile, the four heroes gather at the Galarc Castle, but Sendo Takahisa refuses to train with the others and sets out on his own lonely path to repair his relationship with Miharu. After all, she’s forgiven Aki... So why not him? Will the actions of one pining hero distort the very fabric of the world itself?",
                BookTypeId = 1,
                ReleaseDate = DateTime.ParseExact("September 15, 2023", EntityValidations.BookEntity.ReleaseDate, CultureInfo.InvariantCulture),
                Pages = 223,
                PrintType = PrintTypeEnum.Digital,
                Price = 6.54m,
                FilePath = "/Books/Seirei Gensouki Spirit Chronicles/Seirei Gensouki_ Spirit Chronicles Volume 23.pdf",
                BookTags = new List<BooksTags>()
                {
                    new BooksTags() {TagId = 2, BookId = 3},
                    new BooksTags() { TagId = 1, BookId = 3}
                },
                Orders = new List<Order>()
                {
                    new Order()
                    {
                        Id = Guid.Parse("90e61ca6-b0aa-47f6-8284-666650c464d1"),
                        GameId = null,
                        Status = StatusEnum.Delivered,
                        OrderDate = DateTime.Parse("2024-07-10"),
                        FirstName = "Eva",
                        EmailAddress = "eva@gmail.com",
                        UserId = Guid.Parse("b96cfcbf-d64d-4a38-9fc8-6cf44e9f81e7"),
                        TotalPrice = 6.54m
                    },
                    new Order()
                    {
                        Id = Guid.Parse("1aaf9a1e-ebfd-4e41-8d02-dad95c5db97d"),
                        GameId = null,
                        Status = StatusEnum.Delivered,
                        OrderDate = DateTime.Parse("2024-08-05"),
                        FirstName = "Charlie",
                        EmailAddress = "charlie@gmail.com",
                        UserId = Guid.Parse("7a1d947f-3f7e-4d45-af6e-0874d4e7a0e0"),
                        TotalPrice = 6.54m
                    }
                }
            };

            books.Add(book1);
            books.Add(book2);
            books.Add(book3);

            return books;
        }

        private static IEnumerable<Comment> SeedComments()
        {
            List<Comment> comments = new List<Comment>();

            comment1 = new Comment()
            {
                Id = 1,
                BookId = 1,
                Description = "Test comment 1",
                CreatedDate = DateTime.Now,
                UserId = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                IsDeleted = false,
                UserName = "Test User"
            };

            comment2 = new Comment()
            {
                Id = 2,
                BookId = 1,
                Description = "Test comment 1",
                CreatedDate = DateTime.Now,
                UserId = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                IsDeleted = false,
                UserName = "Test User"
            };

            comment3 = new Comment()
            {
                Id = 3,
                BookId = 2,
                Description = "Test comment 1",
                CreatedDate = DateTime.Now,
                UserId = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                IsDeleted = false,
                UserName = "Test User"
            };

            comments.Add(comment1);
            comments.Add(comment2);
            comments.Add(comment3);

            return comments;
        }

        private static IEnumerable<Tag> SeedTags()
        {
            List <Tag> tags = new List<Tag>();

            tag1 = new Tag()
            {
                Id = 1,
                Name = "Action"
            };

            tag2 = new Tag()
            {
                Id = 2,
                Name = "Adventure"
            };

            tag3 = new Tag()
            {
                Id = 3,
                Name = "Comedy"
            };

            tags.Add(tag1);
            tags.Add(tag2);
            tags.Add(tag3);

            return tags;
        }

        private static IEnumerable<BookType> SeedBookTypes()
        {
            List<BookType> bookTypes = new List<BookType>();

            bookType1 = new BookType()
            {
                Id = 1,
                Name = "Light Novel",
                IsDeleted = false,
            };

            bookType2 = new BookType()
            {
                Id = 2,
                Name = "Manga",
                IsDeleted = false,
            };

            bookTypes.Add(bookType1);
            bookTypes.Add(bookType2);

            return bookTypes;
        }

        private static IEnumerable<FavoriteProducts> SeedFavoriteProducts()
        {
            List<FavoriteProducts> favoriteProducts = new List<FavoriteProducts>()
            {
                new FavoriteProducts()
                {
                    BookId = 1,
                    UserId = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70")
                },
                new FavoriteProducts()
                {
                    BookId = 2,
                    UserId = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70")
                }
            };

            return favoriteProducts;
        }
    }
}
