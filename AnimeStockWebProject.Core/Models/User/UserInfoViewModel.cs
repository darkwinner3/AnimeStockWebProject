using Microsoft.AspNetCore.Http;

namespace AnimeStockWebProject.Core.Models.User
{
    public class UserInfoViewModel : UserViewModel
    {
        public IFormFile? ProfilePictureFile { get; set; }
        public string? ProfilePicturePath { get; set; }
    }
}
