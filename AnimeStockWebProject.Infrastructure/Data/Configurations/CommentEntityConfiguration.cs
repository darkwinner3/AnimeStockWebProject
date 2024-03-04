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
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            IEnumerable<Comment> comments = CreateComments();

            builder.HasData(comments);
            builder.HasOne(c => c.Book)
                .WithMany(b => b.Comments)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Game)
                .WithMany(g => g.Comments)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private IEnumerable<Comment> CreateComments()
        {
            List<Comment> comments = new List<Comment>();

            for (int i = 1; i <= 4; i++)
            {
                Comment comment = new Comment()
                {
                    Id = i,
                    UserId = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                    Description = "Test Comment",
                    CreatedDate = DateTime.Now,
                    BookId = i,
                    GameId = i,
                    UserName = "Test User"
                };
                comments.Add(comment);
            }
            return comments;
        }
    }
}
