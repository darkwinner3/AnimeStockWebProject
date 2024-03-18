using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Comment;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Core.Services
{
    
    public class CommentService : ICommentService
    {
        

        private readonly AnimeStockDbContext animeStockDbContext;

        public CommentService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }

        public async Task<bool> CheckIfCommentExistsByIdAsync(int commentId)
        {
            return await animeStockDbContext.Comments.AnyAsync(c => !c.IsDeleted && c.Id == commentId);
        }

        public async Task CreateCommentAsync(PostCommentViewModel commentViewModel, Guid userId, string userName, bool isCommentingOnBook, bool isCommentingOnGame)
        {
            var comment = new Comment()
            {
                Description = commentViewModel.Description,
                CreatedDate = DateTime.Now,
                UserId = userId,
                UserName = userName,
                BookId = isCommentingOnBook ? commentViewModel.BookId : null,
                GameId = isCommentingOnGame ? commentViewModel.GameId : null
            };
            await animeStockDbContext.Comments.AddAsync(comment);
            await animeStockDbContext.SaveChangesAsync();
        }


        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var comment = await animeStockDbContext.Comments.FirstAsync(c => c.Id == commentId);
            comment.IsDeleted = true;
            await animeStockDbContext.SaveChangesAsync();

            return true;
        }

        public async Task EditCommentAsync(EditCommentViewModel editCommentViewModel)
        {
            var comment = await animeStockDbContext.Comments.FirstAsync(c => c.Id == editCommentViewModel.Id);
            comment.Description = editCommentViewModel.Description;
            await animeStockDbContext.SaveChangesAsync();
        }
    }
}
