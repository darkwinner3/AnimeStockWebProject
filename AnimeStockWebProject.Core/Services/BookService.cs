using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Book.Enum;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using static AnimeStockWebProject.Infrastructure.Data.Enums.PrintTypeEnum;
using Microsoft.EntityFrameworkCore;
using AnimeStockWebProject.Core.Models.Pager;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.Picture;
using AnimeStockWebProject.Core.Models.Comment;

namespace AnimeStockWebProject.Core.Services
{
    public class BookService : IBookService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public BookService( AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await animeStockDbContext.Books.AnyAsync(b => b.Id == bookId && !b.IsDeleted);
        }

        public async Task<AllBooksSortedDataModel> GetAllBooksSortedDataModelAsync(Guid? userId, BookQueryViewModel bookQueryViewModel)
        {
            IQueryable<Book> books = animeStockDbContext.Books
                .Include(b => b.Pictures)
                .Where(b => !b.IsDeleted)
                .AsQueryable();
            books = FilterBooks(bookQueryViewModel, books);
            int pagesToSkip = (bookQueryViewModel.Pager.CurrentPage - 1) * bookQueryViewModel.Pager.PageSize;
            IEnumerable<BookViewModel> allBooks = await books.Skip(pagesToSkip)
                .Take(bookQueryViewModel.Pager.PageSize)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Illustrator = b.Illustrator,
                    Description = b.Description,
                    BookType = b.BookType.Name,
                    ReleaseDate = b.ReleaseDate.Date,
                    PrintType = b.PrintType.ToString(),
                    Price = b.Price,
                    PicturePath = b.Pictures.FirstOrDefault(p => !p.IsDeleted && p.Path.Contains("cover")).Path,
                    IsFavorite = b.FavoriteProducts.Any(fp => fp.BookId == b.Id && fp.UserId == userId)
                }).ToArrayAsync();

            return new AllBooksSortedDataModel()
            {
                Books = allBooks
            };
        }

        public async Task<BookInfoViewModel> GetBookByIdAsync(int bookId, Pager pager)
        {
            int pagesToSkip = (pager.CurrentPage - 1) * pager.PageSize;
            BookInfoViewModel book = await animeStockDbContext
                .Books
                .Select(b => new BookInfoViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Illustrator = b.Illustrator,
                    Description = b.Description,
                    BookType = b.BookType.Name,
                    Publisher = b.Publisher,
                    ReleaseDate = b.ReleaseDate.Date,
                    PrintType = b.PrintType.ToString(),
                    Price = b.Price,
                    Pages = pager.CurrentPage,
                    BookTags = b.BookTags.Where(bt => bt.BookId == bookId && !bt.IsDeleted && !bt.Tag.IsDeleted).Select(b => new TagViewModel()
                    {
                        Name = b.Tag.Name
                    }).ToArray(),
                    Pictures = b.Pictures.Where(p => !p.IsDeleted).OrderByDescending(p => p.Path.Contains("cover")).Select(p => new PictureViewModel()
                    {
                        Path = p.Path
                    }).ToArray(),
                    Comments = b.Comments.Where(c => !c.IsDeleted).Select(c => new CommentViewModel()
                    {
                        Id = c.Id,
                        CreatedOn = c.CreatedDate,
                        Description = c.Description,
                        UserName = c.UserName,
                        UserId = c.UserId,
                        UserPicturePath = c.User.ProfilePicturePath
                    }).Skip(pagesToSkip)
                    .Take(pager.PageSize)
                })
                .FirstAsync(b => b.Id == bookId);
            return book;
        }

        public async Task<int> GetBookCommentsCountAsync(int bookId)
        {
            return await animeStockDbContext.Comments
                .Where(c => c.BookId == bookId && !c.IsDeleted)
                .CountAsync();
        }

        public Task<int> GetCountAsync(BookQueryViewModel bookQueryViewModel)
        {
            IQueryable<Book> books = animeStockDbContext.Books.Where(b => !b.IsDeleted).AsQueryable();
            books = FilterBooks(bookQueryViewModel, books);
            return books.CountAsync();
        }

        private IQueryable<Book> FilterBooks(BookQueryViewModel bookQueryViewModel, IQueryable<Book> books)
        {
            switch (bookQueryViewModel.BookSortEnum)
            {
                case BookSortEnum.ByNewestBook:
                    books = books.OrderByDescending(b => b.ReleaseDate.Date);
                    break;
                case BookSortEnum.ByOldestBook:
                    books = books.OrderBy(b => b.ReleaseDate.Date);
                    break;
                case BookSortEnum.ByAToZ:
                    books = books.OrderBy(b => b.Title);
                    break;
                case BookSortEnum.ByZtoA:
                    books = books.OrderByDescending(b => b.Title);
                    break;
                default:
                    // By default, sort by newest
                    books = books.OrderByDescending(b => b.ReleaseDate.Date);
                    break;
            }

            switch (bookQueryViewModel.PrintType)
            {
                case Default:
                    break;
                case Digital:
                    books = books.Where(b => b.PrintType == Digital);
                    break;
                case Phisycal:
                    books = books.Where(b => b.PrintType == Phisycal);
                    break;   
            }

            if (bookQueryViewModel.SelectedTagIds.Any())
            {
                foreach (var selectedTagId in bookQueryViewModel.SelectedTagIds)
                {
                    books = books.Where(b => b.BookTags.Any(bt => bt.TagId == selectedTagId));
                }
            }

            if (bookQueryViewModel.SelectedBookTypeIds.Any())
            {
                bool allTypesSelected = bookQueryViewModel.SelectedBookTypeIds.Count() == bookQueryViewModel.BookTypes.Count();

                if (!allTypesSelected)
                {
                    foreach (var selectedTypeId in bookQueryViewModel.SelectedBookTypeIds)
                    {
                        books = books.Where(b => b.BookType.Id == selectedTypeId);
                    }
                }
            }

            if (!string.IsNullOrEmpty(bookQueryViewModel.SearchQuery))
            {
                books = books.Where(b => b.Title.Contains(bookQueryViewModel.SearchQuery));
            }

            return books;
        }
    }
}
