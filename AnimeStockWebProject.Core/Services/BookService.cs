using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Book.Enum;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.Comment;
using AnimeStockWebProject.Core.Models.Pager;
using AnimeStockWebProject.Core.Models.Picture;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO.Compression;
using System.Text;
using static AnimeStockWebProject.Infrastructure.Data.Enums.PrintTypeEnum;

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

        public async Task<BookInfoViewModel> GetBookByIdAsync(int bookId, Pager pager, Guid? userId)
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
                    Quantity = b.Quantity,
                    Price = b.Price,
                    Pages = b.Pages,
                    IsFavorite = b.FavoriteProducts.Any(fp => fp.BookId == b.Id && fp.UserId == userId),
                    FilePath = b.FilePath,
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

        public async Task<IEnumerable<BookNameViewModel>> GetBookByTitleAsync(string title, int id)
        {

            // Remove numbers from titles and convert to lowercase
            var cleanedTitle = RemoveNumbersFromString(title).ToLower();

            var books = await animeStockDbContext.Books
                .Where(b => !b.IsDeleted && b.Id != id && b.Title.ToLower().Contains(cleanedTitle))
                .Select(b => new BookNameViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    BookImg = b.Pictures.FirstOrDefault(p => !p.IsDeleted && p.Path.Contains("cover")).Path
                })
                .ToArrayAsync();

            return books;
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

        private string RemoveNumbersFromString(string input)
        {
            var result = new StringBuilder();
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }


        public async Task AddItemToFavorites(int bookId, Guid userId)
        {
            FavoriteProducts favoriteProducts = new FavoriteProducts()
            {
                BookId = bookId,
                UserId = userId
            };
            await animeStockDbContext.FavoriteProducts.AddAsync(favoriteProducts);
            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task RemoveItemFromFavorites(int bookId, Guid userId)
        {
            FavoriteProducts favoriteProducts = await animeStockDbContext
                .FavoriteProducts.FirstAsync(fp => fp.UserId == userId && fp.BookId == bookId);
            animeStockDbContext.FavoriteProducts.Remove(favoriteProducts);

            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task<byte[]> GetBookFileAsync(string filePath, int pageCount)
        {
            //local application location CHANGE ON DIFFERENT PC!
            //string fullPath = @"D:\Important Learning\Programming\web projects\AnimeStockWebProject\AnimeStockWebProject\wwwroot" + filePath.Replace("/", @"\");

            //web application location
            string fullPath = @"C:\home\site\wwwroot\wwwroot\" + filePath;
            string? path = Path.Combine(fullPath);

            if (!File.Exists(fullPath))
            {
                return null;
            }

            using (MemoryStream outputStream = new MemoryStream())
            {
                using (PdfDocument outputDocument = new PdfDocument())
                {
                    if (Path.GetExtension(path).Equals(".cbz", StringComparison.OrdinalIgnoreCase))
                    {
                        using (ZipArchive cbzArchive = ZipFile.OpenRead(path))
                        {
                            int totalPages = Math.Min(pageCount, cbzArchive.Entries.Count);
                            //saves all tasks in a list
                            var tasks = new List<Task>();

                            for (int i = 0; i < totalPages; i++)
                            {
                                ZipArchiveEntry entry = cbzArchive.Entries[i];
                                if (IsImageFile(entry))
                                {
                                    tasks.Add(ProcessImageAsync(entry, outputDocument));
                                }
                            }
                            //exetutes the tasks asynchroniously
                            await Task.WhenAll(tasks);
                        }
                    }
                    else if (Path.GetExtension(path).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                            using (PdfDocument inputDocument = PdfReader.Open(path, PdfDocumentOpenMode.Import))
                            {
                                int totalPages = Math.Min(pageCount, inputDocument.PageCount);

                                for (int i = 0; i < totalPages; i++)
                                {
                                    PdfPage page = inputDocument.Pages[i];
                                    outputDocument.AddPage(page);
                                }
                            }
                    }
                    // Save the output document to the memory stream
                    outputDocument.Save(outputStream);
                }
                // Return the content of the memory stream as a byte array
                return outputStream.ToArray();
            }
        }
        // processes the images asynchroniously
        private async Task ProcessImageAsync(ZipArchiveEntry entry, PdfDocument outputDocument)
        {
            using (Stream imageStream = entry.Open())
            {
                using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(imageStream))
                {
                    PdfPage pdfPage = new PdfPage();

                    outputDocument.AddPage(pdfPage);

                    int targetWidth = (int)pdfPage.Width;
                    int targetHeight = (int)pdfPage.Height;

                    image.Mutate(x => x.Resize(targetWidth, targetHeight));
                    // Convert the ImageSharp image to a MemoryStream with JPEG format
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.SaveAsJpeg(ms);
                        ms.Position = 0;
                        // Create an XImage from the MemoryStream
                        using (XImage xImage = XImage.FromStream(ms))
                        {
                            using (XGraphics gfx = XGraphics.FromPdfPage(pdfPage))
                            {
                                // Draw the image onto the PDF page
                                gfx.DrawImage(xImage, 0, 0);
                            }
                        }
                    }
                }
            }
        }

        private bool IsImageFile(ZipArchiveEntry entry)
        {
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };
            string extenstion = Path.GetExtension(entry.FullName).ToLower();
            return Array.Exists(imageExtensions, ext => ext == extenstion);
        }

        public async Task<BookOrderViewModel> GetBookToOrder(BookInfoViewModel bookInfoViewModel)
        {
            BookOrderViewModel bookToOrder = new BookOrderViewModel()
            {
                Title = bookInfoViewModel.Title,
                BookId = bookInfoViewModel.Id,
                ReleaseDate = bookInfoViewModel.ReleaseDate,
                Price = bookInfoViewModel.Price,
                PrintType = bookInfoViewModel.PrintType,
                UserQuantity = bookInfoViewModel.UserQuantity,
                Quantity = bookInfoViewModel.Quantity,
            };

            bookToOrder.Picture = await animeStockDbContext.Pictures
                .Where(p => p.BookId == bookInfoViewModel.Id && !p.IsDeleted)
                .Select(p => new PictureViewModel()
                {
                    Path = p.Path
                })
                .FirstOrDefaultAsync(p => p.Path.Contains("cover"));

            return bookToOrder;

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
