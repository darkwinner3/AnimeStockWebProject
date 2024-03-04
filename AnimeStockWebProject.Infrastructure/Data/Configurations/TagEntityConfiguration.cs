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
    public class TagEntityConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            ICollection<Tag> tags = CreateTags();
            builder.HasData(tags);
        }

        private ICollection<Tag> CreateTags()
        {
            List<Tag> tags = new List<Tag>()
           {
                new Tag()
                {
                    Id = 1,
                    Name = "Action"
                },
                new Tag()
                {
                    Id= 2,
                    Name = "Adventure"
                },
                new Tag()
                {
                    Id= 3,
                    Name = "Comedy"
                },
                new Tag()
                {
                    Id= 4,
                    Name = "Drama"
                },
                new Tag()
                {
                    Id= 5,
                    Name = "Ecchi"
                },
                new Tag()
                {
                    Id= 6,
                    Name = "Fantasy"
                },
                new Tag()
                {
                    Id= 7,
                    Name = "Harem"
                },
                new Tag()
                {
                    Id= 8,
                    Name = "Historical"
                },
                new Tag()
                {
                    Id= 9,
                    Name = "Horror"
                },
                new Tag()
                {
                    Id= 10,
                    Name = "Martial Arts"
                },
                new Tag()
                {
                    Id= 11,
                    Name = "Mecha"
                },
                new Tag()
                {
                    Id= 12,
                    Name = "Mystery"
                },
                new Tag()
                {
                    Id= 13,
                    Name = "Psychological"
                },
                new Tag()
                {
                    Id= 14,
                    Name = "Romance"
                },
                new Tag()
                {
                    Id= 15,
                    Name = "School Life"
                },
                new Tag()
                {
                    Id= 16,
                    Name = "Sci-fi"
                },
                new Tag()
                {
                    Id= 17,
                    Name = "Supernatural"
                },
                new Tag()
                {
                    Id= 18,
                    Name = "Sports"
                },
                new Tag()
                {
                    Id= 19,
                    Name = "Slice of Life"
                },
                new Tag()
                {
                    Id= 20,
                    Name = "Shounen"
                },
                new Tag()
                {
                    Id= 21,
                    Name = "Isekai"
                },
           };

         return tags;
        }
    }
}
