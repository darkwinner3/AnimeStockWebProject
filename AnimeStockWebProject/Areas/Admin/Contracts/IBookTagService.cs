using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Core.Models.BookTags;

namespace AnimeStockWebProject.Areas.Admin.Contracts
{
    public interface IBookTagService
    {
        Task<IEnumerable<TagViewModel>> GetBookTagsAsync();

        Task<EditBookTagViewModel> GetBookTagToEditAsync(int id);

        Task DeleteBookTagByIdAsync(int id);

        Task RecoverBookTagByIdAsync(int id);

        Task EditBookTagByIdAsync(int id, EditBookTagViewModel editBookTagViewModel);

        Task CreateBookTagAsync(EditBookTagViewModel editBookTagViewModel);
    }
}
