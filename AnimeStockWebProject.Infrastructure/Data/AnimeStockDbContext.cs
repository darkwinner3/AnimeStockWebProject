using AnimeStockWebProject.Infrastructure.Data.Configurations;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Infrastructure.Data
{
    public class AnimeStockDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly bool seedDb;
        public AnimeStockDbContext(DbContextOptions<AnimeStockDbContext> options, bool seedDb = true)
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BooksTags> BookTags { get; set; } = null!;
        public DbSet<BookType> BookTypes { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<FavoriteProducts> FavoriteProducts { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GamesGenres> GameGenres { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Picture> Pictures { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new FavoriteProductsEntityConfiguration());
            builder.ApplyConfiguration(new BookEntityConfiguration());
            builder.ApplyConfiguration(new BooksTagsConfiguration());
            builder.ApplyConfiguration(new BookTypeEntityConfiguration());
            builder.ApplyConfiguration(new CommentEntityConfiguration());
            builder.ApplyConfiguration(new FavoriteProductsEntityConfiguration());
            builder.ApplyConfiguration(new GameEntityConfiguration());
            builder.ApplyConfiguration(new GamesGenresConfiguration());
            builder.ApplyConfiguration(new GenreEntityConfiguration());
            builder.ApplyConfiguration(new PictureEntityConfiguration());
            builder.ApplyConfiguration(new TagEntityConfiguration());
            //if (this.seedDb)
            //{
            //    builder.ApplyConfiguration(new BookEntityConfiguration());
            //    builder.ApplyConfiguration(new BooksTagsConfiguration());
            //    builder.ApplyConfiguration(new BookTypeEntityConfiguration());
            //    builder.ApplyConfiguration(new CommentEntityConfiguration());
            //    builder.ApplyConfiguration(new FavoriteProductsEntityConfiguration());
            //    builder.ApplyConfiguration(new GameEntityConfiguration());
            //    builder.ApplyConfiguration(new GamesGenresConfiguration());
            //    builder.ApplyConfiguration(new GenreEntityConfiguration());
            //    builder.ApplyConfiguration(new PictureEntityConfiguration());
            //    builder.ApplyConfiguration(new TagEntityConfiguration());
            //}
            //else
            //{
            //    builder.Entity<BooksTags>().HasKey(ck => new { ck.TagId, ck.BookId });
            //    builder.Entity<GamesGenres>().HasKey(ck => new { ck.GenreId, ck.GameId });
            //}
        }
    }
}
