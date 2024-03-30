using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Comment;
using AnimeStockWebProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;

namespace AnimeStockWebProject.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] PostCommentViewModel model)
        {
            bool isCommentingOnBook = model.BookId > 0;
            bool isCommentingOnGame = model.GameId > 0;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys
                    .SelectMany(key => ModelState[key].Errors
                    .Select(x => new {Key = key, Error = x.ErrorMessage}))
                    .ToList();
                return Json(new { success = false, errors = errors });
            }

            try
            {
                await commentService.CreateCommentAsync(model, User.GetId(), User.Identity.Name, isCommentingOnBook, isCommentingOnGame);
                TempData[SuccessMessage] = SuccessFullyPostedComment;
                return Json(new { success = true});
            }
            catch (Exception)
            {
                TempData[WarningMessage] = DefaultErrorMessage;
                return Json(new { success = false, });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await commentService.DeleteCommentAsync(id);
                TempData[SuccessMessage] = SuccessRemoveMessage;
                return Json(new { success = true });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] EditCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys
                    .SelectMany(key => ModelState[key].Errors
                    .Select(x => new { Key = key, Error = x.ErrorMessage }))
                    .ToList();
                return Json(new {success = false, errors});
            }
            try
            {
                await commentService.EditCommentAsync(model);
                return Json(new { success = true});
            }
            catch (Exception)
            {
                TempData[WarningMessage] = DefaultErrorMessage;
                return Json(new { success = false });
            }
        }
    }
}
