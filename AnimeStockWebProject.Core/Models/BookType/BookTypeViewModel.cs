using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Core.Models.BookType
{
    public class BookTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
