using AnimeStockWebProject.Infrastructure.Data.Enums;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeStockWebProject.Infrastructure.Data.Configurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.BookType)
                .WithMany(b => b.Books)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(b => b.Orders)
                .WithOne(b => b.Book)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(t => t.BookTags)
                .WithOne(bt => bt.Book)
                .HasForeignKey(t => t.BookId);

            ICollection<Book> booksCollection = CreateBooks();
            builder.HasData(booksCollection);
        }

        private ICollection<Book> CreateBooks()
        {
            List<Book> books = new List<Book>();

            Book bookOne = new Book()
            {
                Id = 1,
                Title = "Date A Live, Vol. 1",
                Author = "Koushi Tachibana",
                Illustrator = "Tsunako",
                Publisher = "Yen On",
                Description = "April 10. The first day of school. Shido Itsuka is rudely awoken by his personal alarm clock—his little sister. It’s shaping up to be another typical day…well, as typical as it gets on a planet plagued by massive spatial quakes. Little does Shido know, however, his life is about to take a sudden turn when he encounters the source of this destructive phenomenon—a girl his age, apparently known as a Spirit. Turns out, there are only two ways he can stop her from unleashing hell on the world: Eliminate her by force or placate her…by taking her out on a date and making her fall in love with him!",
                BookTypeId = 1,
                ReleaseDate = "March 23, 2021",
                Pages = 176,
                Quantity = 7,
                PrintType = PrintTypeEnum.Phisycal,
                Price = 12.23m,
            };
            Book bookTwo = new Book()
            {
                Id = 2,
                Title = "Date A Live, Vol. 2",
                Author = "Koushi Tachibana",
                Illustrator = "Tsunako",
                Publisher = "Yen On",
                Description = "Things have only gotten stranger for Shido ever since Tohka transferred to his school. These days, his life teeters between heaven and hell, and the forecast today points toward the latter. He’s already been caught in the line of fire between Tohka and Origami, and a sudden downpour leaves him sopping wet. Just his luck. Lately, it seems the only place he can catch a break is in the comfort of his own home. That’s all about to change, however, when a new “training” regimen calls for him to live with Tohka—and while Shido is at his most vulnerable, a second Spirit emerges from the storm…",
                BookTypeId = 1,
                ReleaseDate = "May 25, 2021",
                Pages = 184,
                Quantity = 35,
                PrintType = PrintTypeEnum.Phisycal,
                Price = 11.34m,
            };
            Book bookThree = new Book()
            {
                Id = 3,
                Title = "Seirei Gensouki: Spirit Chronicles Volume 23",
                Author = "Yuri Kitayama",
                Illustrator = "Riv",
                Publisher = "J-Novel Club",
                Description = "Rio and Sora journey to the site of Saint Erica’s summoning to find clues about the transcendent rules. As Rio walks in the Saint’s footsteps, more questions arise about the power that the heroes wield.\r\n\r\nMeanwhile, the four heroes gather at the Galarc Castle, but Sendo Takahisa refuses to train with the others and sets out on his own lonely path to repair his relationship with Miharu. After all, she’s forgiven Aki... So why not him? Will the actions of one pining hero distort the very fabric of the world itself?",
                BookTypeId = 1,
                ReleaseDate = "September 15, 2023",
                Pages = 223,
                PrintType = PrintTypeEnum.Digital,
                Price = 6.54m,
            };
            Book bookFour = new Book()
            {
                Id = 4,
                Title = "Mushoku Tensei: Jobless Reincarnation, Vol. 1",
                Author = "Rifujin na Magonote",
                Illustrator = "Shirotaka",
                Publisher = "Seven Seas Entertainment, Seven Seas Siren",
                Description = "Kicked out by his family and wandering the streets, an unemployed 34-year-old shut-in thinks he’s hit rock-bottom—just as he’s hit and killed by a speeding truck! Awakening to find himself reborn as an infant in a world of swords and sorcery, but with the memories of his first life intact, Rudeus Greyrat is determined not to repeat his past mistakes. He’s going to make the most of this reincarnation as he sets off on the adventure of a second lifetime!",
                BookTypeId = 1,
                ReleaseDate = "April 4, 2019",
                Pages = 266,
                PrintType = PrintTypeEnum.Digital,
                Price = 7.12m,
            };
            Book bookFive = new Book()
            {
                Id = 5,
                Title = "Mushoku Tensei: Jobless Reincarnation, Vol. 1",
                Author = "Rifujin Na Magonote",
                Illustrator = "Fujikawa Yuka",
                Publisher = "Seven Seas",
                Description = "An unemployed otaku has just reached the lowest point in his life. He wants nothing more than the ability to start over, but just as he thinks it may be possible...he gets hit by a truck and dies! Shockingly, he finds himself reborn into an infant’s body in a strange new world of swords and magic. His identity now is Rudeus Greyrat, yet he still retains the memories of his previous life. Reborn into a new family, Rudeus makes use of his past experiences to forge ahead in this fantasy world as a true prodigy, gifted with maturity beyond his years and a natural born talent for magic. With swords instead of chopsticks, and spell books instead of the internet, can Rudeus redeem himself in this wondrous yet dangerous land?",
                BookTypeId = 2,
                ReleaseDate = "November 24, 2015",
                Pages = 180,
                Quantity = 13,
                PrintType = PrintTypeEnum.Phisycal,
                Price = 10.23m,
            };
            Book bookSix = new Book()
            {
                Id = 6,
                Title = "Chainsaw Man, Vol. 13",
                Author = "Tatsuki Fujimoto",
                Illustrator = "Tatsuki Fujimoto",
                Publisher = "VIZ Media LLC",
                Description = "Denji was a small-time devil hunter just trying to survive in a harsh world. After being killed on a job, he is revived by his pet devil Pochita and becomes something new and dangerous—Chainsaw Man! Denji is desperate to tell the world that he’s Chainsaw Man, but is he competent enough to pull off a proper reveal? Meanwhile, Asa has made a friend! But this new friendship may be hiding a dark secret.",
                BookTypeId = 2,
                ReleaseDate = "December 5, 2023",
                Pages = 184,
                Quantity = 53,
                PrintType = PrintTypeEnum.Phisycal,
                Price = 9.76m,
            };

            books.Add(bookOne);
            books.Add(bookTwo);
            books.Add(bookThree);
            books.Add(bookFour);
            books.Add(bookFive);
            books.Add(bookSix);

            return books;
        } 
    }
}
