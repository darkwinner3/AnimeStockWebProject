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
    public class PictureEntityConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasOne(p => p.Book)
                .WithMany(b => b.Pictures)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Game)
                .WithMany(g => g.Pictures)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

            ICollection<Picture> pictures = CreatePictures();
            builder.HasData(pictures);
        }

        private ICollection<Picture> CreatePictures()
        {
            List<Picture> pictures = new List<Picture>()
            {
                new Picture()
                {
                    Id = 1,
                    BookId = 1,
                    Path = "/img/Books/Date a Live Vol 1(Light Novel)/picture1.png"
                },
                new Picture()
                {
                    Id = 2,
                    BookId = 1,
                    Path = "/img/Books/Date a Live Vol 1(Light Novel)/picture2.png"
                },
                new Picture()
                {
                    Id = 3,
                    BookId = 1,
                    Path = "/img/Books/Date a Live Vol 1(Light Novel)/picture3.png"
                },
                new Picture()
                {
                    Id = 4,
                    BookId = 2,
                    Path = "/img/Books/Date a Live Vol 2(Light Novel)/Picture_1.png"
                },
                new Picture()
                {
                    Id = 5,
                    BookId = 2,
                    Path = "/img/Books/Date a Live Vol 2(Light Novel)/Picture_2.png"
                },
                new Picture()
                {
                    Id = 6,
                    BookId = 2,
                    Path = "/img/Books/Date a Live Vol 2(Light Novel)/Picture_3.png"
                },
                new Picture()
                {
                    Id = 7,
                    BookId = 3,
                    Path = "/img/Books/Spirit Chronicles Vol 23(Light Nove)/picture1.png"
                },
                new Picture()
                {
                    Id = 8,
                    BookId = 3,
                    Path = "/img/Books/Spirit Chronicles Vol 23(Light Nove)/picture2.png"
                },
                new Picture()
                {
                    Id = 9,
                    BookId = 3,
                    Path = "/img/Books/Spirit Chronicles Vol 23(Light Nove)/picture3.png"
                },
                new Picture()
                {
                    Id = 10,
                    BookId = 4,
                    Path = "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/picture 1.png"
                },
                new Picture()
                {
                    Id = 11,
                    BookId = 4,
                    Path = "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/picture 2.png"
                },
                new Picture()
                {
                    Id = 12,
                    BookId = 4,
                    Path = "/img/Books/Mushoku Tensei Vol 1 (Light Novel)/picture 3.png"
                },
                new Picture()
                {
                    Id = 13,
                    BookId = 5,
                    Path = "/img/Books/Mushoku Tensei Vol 1 (Manga)/81vgliRXgRL._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 14,
                    BookId = 5,
                    Path = "/img/Books/Mushoku Tensei Vol 1 (Manga)/91yoqb7q5nL._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 15,
                    BookId = 5,
                    Path = "/img/Books/Mushoku Tensei Vol 1 (Manga)/91yoqb7q5nL._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 16,
                    BookId = 6,
                    Path = "/img/Books/Chainsaw Man, Vol. 13(Manga)/8194kTgN5iL._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 17,
                    BookId = 6,
                    Path = "/img/Books/Chainsaw Man, Vol. 13(Manga)/81WO4SsaNzL._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 18,
                    GameId = 1,
                    Path = "/img/Games/Atelier Ryza 2/MV5BMTAxMDQzNDMtMjFkMC00NWNmLWE0NjItZDdkMWNjYmZkNjhlXkEyXkFqcGdeQXVyNzgxNDk0NTI@._V1_.jpg"
                },
                new Picture()
                {
                    Id = 19,
                    GameId = 1,
                    Path = "/img/Games/Atelier Ryza 2/ss_38608a834caa5243f66945693ccc1eddee35942b.1920x1080.jpg"
                },
                new Picture()
                {
                    Id = 20,
                    GameId = 1,
                    Path = "/img/Games/Atelier Ryza 2/ss_a0d278df03b540c0f21d2cae130ca30b296353ca.1920x1080.jpg"
                },
                new Picture()
                {
                    Id = 21,
                    GameId = 1,
                    Path = "/img/Games/Atelier Ryza 2/ss_e6b1fd3eb46f1ea035b758c991bda2f5e15673c4.1920x1080.jpg"
                },
                new Picture()
                {
                    Id = 22,
                    GameId = 2,
                    Path = "/img/Games/STEINS GATE/2611526-untitled.png"
                },
                new Picture()
                {
                    Id = 23,
                    GameId = 2,
                    Path = "/img/Games/ss_18e33276a72437c7728c969fe079e7bda6b7fb08.1920x1080.png"
                },
                new Picture()
                {
                    Id = 24,
                    GameId = 2,
                    Path = "/img/Games/STEINS GATE/ss_48523dbba66bc7ea79c8bc46eabaee6190e005e9.1920x1080.png"
                },
                new Picture()
                {
                    Id = 25,
                    GameId = 2,
                    Path = "/img/Games/STEINS GATE/ss_56d0a07b2ca57c72eb9c74ca918d49e3cf7a06a6.1920x1080.png"
                },
                new Picture()
                {
                    Id = 26,
                    GameId = 3,
                    Path = "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/71d7tEoKY6L._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 27,
                    GameId = 3,
                    Path = "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/71SUTICB69L._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 28,
                    GameId = 3,
                    Path = "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/81VGg56E0zL._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 29,
                    GameId = 3,
                    Path = "/img/Games/FINAL FANTASY VII REMAKE INTERGRADE/81W8CAno24L._SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 30,
                    GameId = 4,
                    Path = "/img/Games/Little Witch Nobeta/71+q7XyABPL._AC_SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 31,
                    GameId = 4,
                    Path = "/img/Games/Little Witch Nobeta/71rB0zyXdVL._AC_SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 32,
                    GameId = 4,
                    Path = "/img/Games/Little Witch Nobeta/71zb3knEnaL._AC_SL1500_.jpg"
                },
                new Picture()
                {
                    Id = 33,
                    GameId = 4,
                    Path = "/img/Games/Little Witch Nobeta/91f4gT-pGyL._AC_SL1500_.jpg"
                },
            };
            return pictures;
        }
    }
}
