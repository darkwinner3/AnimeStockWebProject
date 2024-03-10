using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.BookType;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Core.Services
{
    public class TypeService : ITypeService
    {
        private readonly AnimeStockDbContext animeStockDb;

        public TypeService(AnimeStockDbContext animeStockDb)
        {
            this.animeStockDb = animeStockDb;
        }
        public async Task<IEnumerable<BookTypeViewModel>> GetAllTypesAsync()
        {
            IEnumerable<BookTypeViewModel> types = await animeStockDb
                .BookTypes.Where(t => !t.IsDeleted)
                .Select(t => new BookTypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToArrayAsync();
            return types;
        }
    }
}
