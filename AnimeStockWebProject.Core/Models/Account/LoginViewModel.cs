namespace AnimeStockWebProject.Core.Models.Account
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter Username or Email")]
        [DisplayName("Username or Email")]
        public string UsernameOrEmail { get; set; } = null!;

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
