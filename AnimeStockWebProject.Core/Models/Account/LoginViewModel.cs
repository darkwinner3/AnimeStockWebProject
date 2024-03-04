namespace AnimeStockWebProject.Core.Models.Account
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Username or Email")]
        public string UsernameOrEmail { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
