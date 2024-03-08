using AnimeStockWebProject.Core.Models.BookTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetAllTagsAsync();
    }
}
