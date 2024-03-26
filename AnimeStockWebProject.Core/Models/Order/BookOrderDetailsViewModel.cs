using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.User;
using System.ComponentModel.DataAnnotations;

namespace AnimeStockWebProject.Core.Models.Order
{
    public class BookOrderDetailsViewModel
    {
        public BookOrderViewModel BookInfo { get; set; } = null!;
        public int UserQuantity { get; set; }
        public UserViewModel User { get; set; } = null!;

        public ValidationResult Validate(ValidationContext validationContext)
        {
            if (UserQuantity > BookInfo.Quantity)
            {
                return new ValidationResult("Order quantity exceeded book quantity");
            }

            return null;
        }
    }
}
