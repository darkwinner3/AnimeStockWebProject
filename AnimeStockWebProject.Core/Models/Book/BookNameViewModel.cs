namespace AnimeStockWebProject.Core.Models.Book
{
    public class BookNameViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public string BookImg { get; set; } = null!;
    }
}
