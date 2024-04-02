using AnimeStockWebProject.Core.Models.Picture;

namespace AnimeStockWebProject.Areas.Admin.Models.Pictures
{
    public class PictureAdminViewModel : PictureViewModel
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
