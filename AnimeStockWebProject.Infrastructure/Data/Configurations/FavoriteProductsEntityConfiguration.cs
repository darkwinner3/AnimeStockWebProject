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
            builder.HasKey(ck => new { ck.UserId, ck.BookId, ck.GameId });
            builder.HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(fg => fg.Game)
                .WithMany(g => g.FavoriteProducts)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(fb => fb.Book)
                .WithMany(b => b.FavoriteProducts)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
