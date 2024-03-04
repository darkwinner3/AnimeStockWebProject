using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Infrastructure.Data.Configurations
{
    public class BooksTagsConfiguration : IEntityTypeConfiguration<BooksTags>
    {
        public void Configure(EntityTypeBuilder<BooksTags> builder)
        {
            builder.HasKey(ck => new { ck.BookId, ck.TagId });
            builder.Property(bt => bt.IsDeleted).HasDefaultValue(false);
            builder.HasOne(bt  => bt.Tag)
                .WithMany(t => t.BookTags)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(bt => bt.Book)
                .WithMany(b => b.BookTags)
                .OnDelete(DeleteBehavior.NoAction);
            ICollection<BooksTags> bookTags = CreateBookTags();
            
            builder.HasData(bookTags);
        }

        private ICollection<BooksTags> CreateBookTags()
        {
            List<BooksTags> bookTags = new List<BooksTags>();
            bookTags.AddRange(AddTagsToBook(1, new int[] { 1, 3, 4, 5, 7, 14, 15 }));
            bookTags.AddRange(AddTagsToBook(2, new int[] { 1, 3, 4, 5, 7, 14, 15, 16 }));
            bookTags.AddRange(AddTagsToBook(3, new int[] { 21, 14, 7, 6, 2, 1 }));
            bookTags.AddRange(AddTagsToBook(4, new int[] { 21, 1, 2, 5, 6, 7, 14 }));
            bookTags.AddRange(AddTagsToBook(5, new int[] { 21, 1, 2, 5, 6, 7, 14 }));
            bookTags.AddRange(AddTagsToBook(6, new int[] { 15, 17, 13, 9, 4, 1 }));
            
            return bookTags;
        }

        private ICollection<BooksTags> AddTagsToBook(int bookId, int[] tagIds)
        {
            List<BooksTags> bookTags = new List<BooksTags>();

            foreach (int tagId in tagIds)
            {
                BooksTags bookTag = new BooksTags()
                {
                    BookId = bookId,
                    TagId = tagId
                };
                bookTags.Add(bookTag);
            }
            return bookTags;
        }
    }
}
