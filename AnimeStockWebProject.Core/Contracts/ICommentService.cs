using AnimeStockWebProject.Core.Models.Comment;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface ICommentService
    {
        Task CreateCommentAsync(PostCommentViewModel commentViewModel, Guid userId, string userName,bool isCommentingOnGame, bool isCommentingOnBook);

        Task<bool> CheckIfCommentExistsByIdAsync(int commentId);

        Task<bool> DeleteCommentAsync(int commentId);

        Task EditCommentAsync(EditCommentViewModel editCommentViewModel);
    }
}
