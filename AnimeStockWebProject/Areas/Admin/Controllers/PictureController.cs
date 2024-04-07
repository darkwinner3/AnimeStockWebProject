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
        public async Task<IActionResult> Delete(int id)
        {
            if (!await pictureAdminService.PictureExistsByIdAsync(id))
            {
                return NotFound();
            }
            if (await pictureAdminService.PictureIsAlreadyDeletedAsync(id))
            {
                return Json(new { success = false });
            }
            try
            {
                await pictureAdminService.DeletePictureAsync(id);
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
        public async Task<IActionResult> Recover(int id)
        {
            if (!await pictureAdminService.PictureExistsByIdAsync(id))
            {
                return NotFound();
            }
            if (await pictureAdminService.PictureIsRecoveredAsync(id))
            {
                return Json(new { success = false });
            }
            try
            {
                await pictureAdminService.RecoverPictureAsync(id);
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
