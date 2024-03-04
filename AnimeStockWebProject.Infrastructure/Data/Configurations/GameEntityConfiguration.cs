using AnimeStockWebProject.Infrastructure.Data.Enums;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeStockWebProject.Infrastructure.Data.Configurations
{
    public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasMany(g => g.Orders)
                .WithOne(g => g.Game)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g => g.GameGenres)
                .WithOne(gg => gg.Game)
                .HasForeignKey(g => g.GameId);

            ICollection<Game> gamesCollection = CreateGames();
            builder.HasData(gamesCollection);
        }

        private ICollection<Game> CreateGames()
        {
            List<Game> games = new List<Game>();

            Game gameOne = new Game()
            {
                Id = 1,
                Name = "Atelier Ryza 2: Lost Legends & the Secret Fairy",
                Publisher = "KOEI TECMO GAMES CO., LTD.",
                Developer = "KOEI TECMO GAMES CO., LTD.",
                Description = "This story takes place three years after the events of the previous game “Atelier Ryza: Ever Darkness & the Secret Hideout,” and depicts the reunion of Ryza and her friends, who go through new encounters and goodbyes to discover a true priceless treasure.",
                ReleaseDate = "Jan 26, 2021",
                PrintType = PrintTypeEnum.Digital,
                Price = 59.99m,
                platformType = PlatformTypeEnum.PC,
            };
            Game gameTwo = new Game()
            {
                Id = 2,
                Name = "STEINS;GATE",
                Publisher = "Spike Chunsoft Co., Ltd.",
                Developer = "MAGES. Inc.",
                Description = "STEINS;GATE follows a rag-tag band of tech-savvy young students who discover the means of changing the past via mail, using a modified microwave. Their experiments into how far they can go with their discovery begin to spiral out of control as they become entangled in a conspiracy surrounding SERN, the organisation behind the Large Hadron Collider, and John Titor who claims to be from a dystopian future.",
                ReleaseDate = "Sep 9, 2016",
                PrintType = PrintTypeEnum.Digital,
                Price = 26.99m,
                platformType = PlatformTypeEnum.PC,
            };
            Game gameThree = new Game()
            {
                Id = 3,
                Name = "FINAL FANTASY VII REMAKE INTERGRADE",
                Publisher = "Square Enix",
                Developer = "Square Enix",
                Description = "Final FANTASY VII remake intergrade is an enhanced and expanded version of the critically acclaimed and award-winning final FANTASY VII remake for the PlayStation 5. Final FANTASY VII remake intergrade comes bundled with the brand-new episode featuring yuffie as the main character which introduces an exhilarating new story Arc, and numerous gameplay additions for players to enjoy. The world has fallen under the control of the Shinra electric power company, a shadowy Corporation controlling the planet's very life force as Mako energy. In the sprawling city of Midgar, an anti-shinra organization calling themselves Avalanche have stepped up their resistance. Cloud strife, a former member of shinra's Elite soldier unit now turned mercenary, lends his aid to the group, Unaware of the epic consequences that await him. The new episode featuring yuffie is a brand-new adventure in the world of FINAL FANTASY VII remake intergrade. Play as wutai Ninja yuffie kisaragi as she infiltrates midgar and conspiracies with Avalanche HQ to steal the ultimate materia from the Shinra electric power company. Play alongside new characters and enjoy an expanded gameplay experience featuring multiple new combat and gameplay additions. This adventure brings new perspective to the final FANTASY VII remake story that cannot be missed.",
                ReleaseDate = "March 1, 2021",
                PrintType = PrintTypeEnum.Phisycal,
                Price = 35.99m,
                platformType = PlatformTypeEnum.PlayStation5,
            };
            Game gameFour = new Game()
            {
                Id = 4,
                Name = "Little Witch Nobeta",
                Publisher = "Pupuya Games",
                Developer = "Pupuya Games, SimonCreative, Justdan",
                Description = "The Little Witch Nobeta is a 3D action shooting game. Players will explore ancient, unknown castles and use different magic elements to fight against the soul!\r\nThe game uses a comfortable Japanese art style, but the battles are quite challenging despite its cute looks. Underestimating your foes will lead to troublesome encounters. You must discover enemies' weaknesses and learn the precise time to dodge attacks in order to gain the advantage in combat.",
                ReleaseDate = "November 2, 2022",
                PrintType = PrintTypeEnum.Phisycal,
                Price = 34.00m,
                platformType = PlatformTypeEnum.NintendoSwitch,
            };
            games.Add(gameOne);
            games.Add(gameTwo);
            games.Add(gameThree);
            games.Add(gameFour);

            return games;
        }
    }
}
