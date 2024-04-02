namespace AnimeStockWebProject.Core.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using static AnimeStockWebProject.Common.EntityValidations.UserEntity;
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter Username")]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength, ErrorMessage = "Username must be between 5 and 35 characters long")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength , ErrorMessage = "Password must be between 5 and 30 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [EmailAddress]
        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(EmailAddressMaxValue, MinimumLength = EmailAddressMinValue)]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
