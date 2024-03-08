using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Core.Models.Book
{
    public class AllBooksSortedDataModel
    {
        public AllBooksSortedDataModel()
        {
            Books = new List<BookViewModel>();
        }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
