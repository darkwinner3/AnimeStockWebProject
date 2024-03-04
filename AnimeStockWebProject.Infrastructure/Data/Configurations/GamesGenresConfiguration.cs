using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Infrastructure.Data.Configurations
{
    public class GamesGenresConfiguration : IEntityTypeConfiguration<GamesGenres>
    {
        public void Configure(EntityTypeBuilder<GamesGenres> builder)
        {
            builder.HasKey(ck => new { ck.GameId, ck.GenreId });
            builder.Property(gg => gg.IsDeleted).HasDefaultValue(false);

            builder.HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .OnDelete(DeleteBehavior.NoAction);
            ICollection<GamesGenres> gameGenres = CreateGameGenres();
            builder.HasData(gameGenres);
        }

        private ICollection<GamesGenres> CreateGameGenres()
        {
            List<GamesGenres> gameGenres = new List<GamesGenres>();
            gameGenres.AddRange(AddGenresToGame(1, new int[] { 3, 5, 8, 10, 15 }));
            gameGenres.AddRange(AddGenresToGame(2, new int[] { 11, 8, 1 }));
            gameGenres.AddRange(AddGenresToGame(3, new int[] { 3, 5, 7, 8, 10, 16 }));
            gameGenres.AddRange(AddGenresToGame(4, new int[] { 16, 14, 10, 7 }));
            return gameGenres;
        }

        private ICollection<GamesGenres> AddGenresToGame(int gameId, int[] genreIds)
        {
            List<GamesGenres> gameGenres = new List<GamesGenres>();

            foreach (int genreId in genreIds)
            {
                GamesGenres gameGenre = new GamesGenres()
                {
                    GameId = gameId,
                    GenreId = genreId
                };
                gameGenres.Add(gameGenre);
            }
            return gameGenres;
        }
    }
}
