namespace AnimeStockWebProject.Areas.Admin.Contracts
{
    public interface IPictureAdminService
    {
        Task<bool> PictureExistsByIdAsync(int pictureId);

        Task<bool>  DeletePictureAsync(int pictureId);

        Task RecoverPictureAsync(int pictureId);

        Task<bool> PictureIsAlreadyDeletedAsync(int pictureId);

        Task<bool> PictureIsRecoveredAsync(int pictureId);

        Task DeletePicturesAsync();
    }
}
