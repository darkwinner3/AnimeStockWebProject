namespace AnimeStockWebProject.Areas.Admin.Models.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int BookTypeId { get; set; }
        public string Title { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
