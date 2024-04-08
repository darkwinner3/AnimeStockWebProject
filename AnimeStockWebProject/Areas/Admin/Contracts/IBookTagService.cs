using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Core.Models.BookTags;

namespace AnimeStockWebProject.Areas.Admin.Contracts
{
    public interface IBookTagService
    {
        Task<IEnumerable<TagViewModel>> GetBookTagsAsync();

        Task<EditBookTagViewModel> GetBookTagToEditAsync();
    }
}
