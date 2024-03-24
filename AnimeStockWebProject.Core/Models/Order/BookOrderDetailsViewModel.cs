using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Core.Models.Order
{
    public class BookOrderDetailsViewModel
    {
        public BookOrderViewModel BookInfo { get; set; } = null!;

        public DateTime? OrderDate { get; set; }

        public UserViewModel User { get; set; } = null!;
    }
}
