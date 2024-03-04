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
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            ICollection<Genre> genres = CreateGenres();
            builder.HasData(genres);
        }

        private ICollection<Genre> CreateGenres()
        {
            List<Genre> genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1,
                    Name = "Visual Novel",
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Dating Sim",
                },
                new Genre()
                {
                    Id = 3,
                    Name = "JRPG",
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Puzzle",
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Party-Based RPG",
                },
                new Genre()
                {
                    Id = 6,
                    Name = "Interactive Fiction",
                },
                new Genre()
                {
                    Id = 7,
                    Name = "Action",
                },
                new Genre()
                {
                    Id = 8,
                    Name = "Story Rich",
                },
                new Genre()
                {
                    Id = 9,
                    Name = "RPG",
                },
                new Genre()
                {
                    Id = 10,
                    Name = "Adventure",
                },
                new Genre()
                {
                    Id = 11,
                    Name = "Psychological Horror",
                },
                new Genre()
                {
                    Id = 12,
                    Name = "Casual",
                },
                new Genre()
                {
                    Id = 13,
                    Name = "Multiplayer",
                },
                new Genre()
                {
                    Id = 14,
                    Name = "Singleplayer",
                },
                new Genre()
                {
                    Id = 15,
                    Name = "Open World",
                },
                new Genre()
                {
                    Id = 16,
                    Name = "Fantasy",
                },
            };
            return genres;
        }
    }
}
