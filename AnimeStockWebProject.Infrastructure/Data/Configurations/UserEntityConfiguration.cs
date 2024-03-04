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
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            IEnumerable<User> users = CreateUsers();
            builder.HasData(users);
            builder.Property(u => u.JoinTime).HasDefaultValue(DateTime.Now);
        }

        private IEnumerable<User> CreateUsers()
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User userOne = new User()
            {
                Id = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                UserName = "Test User",
                NormalizedUserName = "TEST USER",
                Email = "testuser123@gmail.com",
                NormalizedEmail = "TESTUSER123@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                JoinTime = DateTime.Now,
            };

            User adminUser = new User()
            {
                Id = Guid.Parse("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin123@gmail.com",
                NormalizedEmail = "ADMIN123@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                JoinTime = DateTime.Now,
            };

            userOne.PasswordHash = passwordHasher.HashPassword(userOne, "test12345");
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin12345");
            List<User> users = new List<User>() { userOne, adminUser };

            return users;
        }
    }
}
