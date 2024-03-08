namespace AnimeStockWebProject.Core.Models.BookTags
{
    public class TagViewModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
