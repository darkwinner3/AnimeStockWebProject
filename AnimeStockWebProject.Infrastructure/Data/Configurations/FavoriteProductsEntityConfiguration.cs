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
    public class FavoriteProductsEntityConfiguration : IEntityTypeConfiguration<FavoriteProducts>
    {
        public void Configure(EntityTypeBuilder<FavoriteProducts> builder)
        {
            builder.HasKey(ck => new { ck.Id });

            builder.HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(fp => fp.UserId)
                .IsRequired();

            // Configure BookId and GameId as regular nullable foreign keys
            builder.HasOne(fg => fg.Game)
                .WithMany(g => g.FavoriteProducts)
                .HasForeignKey(fp => fp.GameId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(fb => fb.Book)
                .WithMany(b => b.FavoriteProducts)
                .HasForeignKey(fp => fp.BookId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
