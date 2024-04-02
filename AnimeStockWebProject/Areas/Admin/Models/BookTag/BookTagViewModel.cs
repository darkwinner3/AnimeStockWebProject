namespace AnimeStockWebProject.Areas.Admin.Models.BookTag
{
    public class BookTagViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
