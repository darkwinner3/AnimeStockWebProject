using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.BookType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface ITypeService
    {
        Task<IEnumerable<BookTypeViewModel>> GetAllTypesAsync();
    }
}
