using AnimeStockWebProject.Areas.Admin.Contracts;
using Microsoft.AspNetCore.Mvc;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;

namespace AnimeStockWebProject.Areas.Admin.Controllers
{
    public class PictureController : AdminController
    {
        private readonly IPictureAdminService pictureAdminService;

        public PictureController(IPictureAdminService pictureAdminService)
        {
            this.pictureAdminService = pictureAdminService;
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int pictureId)
        {
            if (!await pictureAdminService.PictureExistsByIdAsync(pictureId))
            {
                return NotFound();
            }
            if (await pictureAdminService.PictureIsAlreadyDeletedAsync(pictureId))
            {
                return Json(new { success = false });
            }
            try
            {
                await pictureAdminService.DeletePictureAsync(pictureId);
                TempData[SuccessMessage] = SuccessfullyDeletedImage;
                return Json(new { success = true });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Recover([FromBody] int pictureId)
        {
            if (!await pictureAdminService.PictureExistsByIdAsync(pictureId))
            {
                return NotFound();
            }
            if (await pictureAdminService.PictureIsRecoveredAsync(pictureId))
            {
                return Json(new { success = false });
            }
            try
            {
                await pictureAdminService.RecoverPictureAsync(pictureId);
                TempData[SuccessMessage] = SuccessfullyRecoveredImage;
                return Json(new { success = true });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return Json(new { success = false });
            }
        }
    }
}
