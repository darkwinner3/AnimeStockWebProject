using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Infrastructure.Data.Configurations
{
    public class BookTypeEntityConfiguration : IEntityTypeConfiguration<BookType>
    {
        public void Configure(EntityTypeBuilder<BookType> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

            ICollection<BookType> bookTypes = CreateBookTypes();
            builder.HasMany(bt => bt.Books)
                .WithOne(bt => bt.BookType)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasData(bookTypes);
        }

        private ICollection<BookType> CreateBookTypes()
        {
            List<BookType> bookTypes = new List<BookType>()
            {
                new BookType
                {
                    Id = 1,
                    Name = "Light Novel",
                },
                new BookType
                {
                    Id= 2,
                    Name = "Manga"
                }
            };
            return bookTypes;
        }
    }
}
