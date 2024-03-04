namespace AnimeStockWebProject.Core.Models.User
{
    using AnimeStockWebProject.Infrastructure.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidations.UserEntity;
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(FirstNameMaxValue, MinimumLength = FirstNameMinValue)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
