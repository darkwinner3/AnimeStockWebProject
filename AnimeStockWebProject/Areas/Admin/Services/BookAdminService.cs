using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Core.Models.Pager;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class BookAdminService : IBookAdminService
    {
        private readonly IWebHostEnvironment env;
        private readonly AnimeStockDbContext animeStockDbContext;

        public BookAdminService(AnimeStockDbContext animeStockDbContext, IWebHostEnvironment env)
        {
            this.animeStockDbContext = animeStockDbContext;
            this.env = env;
        }

        public async Task<int> BookAddAsync(BookAddViewModel bookAddViewModel)
        {
            BookType bookType = await animeStockDbContext.BookTypes.FirstAsync(bt => bt.Id == bookAddViewModel.BookTypeId);
            Book bookToAdd = new Book()
            {
                Author = bookAddViewModel.Author,
                Description = WebUtility.HtmlEncode(bookAddViewModel.Description),
                IsDeleted = false,
                ReleaseDate = bookAddViewModel.ReleaseDate,
                Illustrator = bookAddViewModel.Illustrator,
                Pages = bookAddViewModel.Pages,
                Price = bookAddViewModel.Price,
                Publisher = bookAddViewModel.Publisher,
                Quantity = bookAddViewModel.Quantity,
                Title = bookAddViewModel.Title,
                PrintType = bookAddViewModel.PrintType,
                BookTypeId = bookAddViewModel.BookTypeId
            };

            await animeStockDbContext.Books.AddAsync(bookToAdd);
            await animeStockDbContext.SaveChangesAsync();

            if (bookAddViewModel.SelectedBookTagIds.Any())
            {
                foreach (var bookTag in bookAddViewModel.SelectedBookTagIds)
                {
                    await animeStockDbContext.BookTags.AddAsync(new BooksTags() 
                    { 
                        TagId = bookTag, 
                        BookId = bookToAdd.Id
                    });
                }
            }
            await animeStockDbContext.SaveChangesAsync();
            return bookToAdd.Id;
        }

        public async Task CreateBookFileAsync(int bookId, BookAddViewModel bookAddViewModel)
        {
            bool mangaBookFolderExists = false;
            bool bookFolderExists = false;
            var book = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);
            var fileType = Path.GetExtension(bookAddViewModel.BookFile.FileName);
            string directoryPath = Path.Combine(env.WebRootPath, "Books");
            
            string bookTitle = ReplaceInvalidCharacters(book.Title);
            string coreTitle = ExtractCoreTitle(bookTitle);
            if (fileType == ".cbz")
            {
                coreTitle += " (manga)";
            }
            string uploadPath = Path.Combine(directoryPath, coreTitle);

            string[] folderPaths = Directory.GetDirectories(directoryPath);
            foreach (var folderPath in folderPaths)
            {
                string folderName = Path.GetFileName(folderPath);
                if (bookTitle.ToLower().Contains(folderName.ToLower()) && fileType == ".cbz" && folderName.ToLower().Contains("manga")) 
                {
                    string fileName = Path.GetFileName(bookAddViewModel.BookFile.FileName);
                    string filePath = Path.Combine(directoryPath, folderName, fileName);
                    string bookFilePath = Path.Combine("Books", folderName, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        book.FilePath = bookFilePath;
                        await animeStockDbContext.SaveChangesAsync();
                        await bookAddViewModel.BookFile.CopyToAsync(stream);
                    }
                    mangaBookFolderExists = true;
                    break;
                }
                else if (bookTitle.ToLower().Contains(folderName.ToLower()) && fileType == ".pdf")
                {
                    string fileName = Path.GetFileName(bookAddViewModel.BookFile.FileName);
                    string filePath = Path.Combine(directoryPath, folderName, fileName);
                    string bookFilePath = Path.Combine("Books", folderName, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        book.FilePath = bookFilePath;
                        await animeStockDbContext.SaveChangesAsync();
                        await bookAddViewModel.BookFile.CopyToAsync(stream);
                    }
                    bookFolderExists = true;
                    break;
                }
            }
            if (!bookFolderExists && fileType == ".pdf")
            {
                Directory.CreateDirectory(uploadPath);

                string fileName = Path.GetFileName(bookAddViewModel.BookFile.FileName);
                string filePath = Path.Combine(directoryPath, coreTitle, fileName);
                string bookFilePath = Path.Combine("Books", coreTitle, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    book.FilePath = bookFilePath;
                    await animeStockDbContext.SaveChangesAsync();
                    await bookAddViewModel.BookFile.CopyToAsync(stream);
                }
            }
            if (!mangaBookFolderExists && fileType == ".cbz")
            {
                Directory.CreateDirectory(uploadPath);

                string fileName = Path.GetFileName(bookAddViewModel.BookFile.FileName);
                string filePath = Path.Combine(directoryPath, coreTitle, fileName);
                string bookFilePath = Path.Combine("Books", coreTitle, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    book.FilePath = bookFilePath;
                    await animeStockDbContext.SaveChangesAsync();
                    await bookAddViewModel.BookFile.CopyToAsync(stream);
                }
            }
        }
        private string ExtractCoreTitle(string fullTitle)
        {
            string[] separators = { ",", "(" }; // Splitting by comma or (
            string[] parts = fullTitle.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return parts[0].Trim(); // Taking the first part as core title
        }

        public async Task CreateBookPicturesAsync(int bookId, BookAddViewModel bookAddViewModel)
        {
            var book = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);

            string bookFolderName = ReplaceInvalidCharacters(book.Title);
            string uploadPath = Path.Combine(env.WebRootPath, "img", "Books", bookFolderName);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            foreach (var img in bookAddViewModel.Pictures!)
            {
                if (img.Length > 0)
                {
                    string imgName = Path.GetFileName(img.FileName);
                    string imgPath = Path.Combine(uploadPath, imgName);
                    using (FileStream stream = new FileStream(imgPath, FileMode.Create))
                    {
                        await animeStockDbContext.Pictures.AddAsync(new Picture()
                        {
                            Path = $"/img/Books/{bookFolderName}/{imgName}",
                            BookId = bookId,
                        });
                        await animeStockDbContext.SaveChangesAsync();
                        await img.CopyToAsync(stream);
                    }
                }
            }
        }

        public async Task CreateCoverImgAsync(int bookId, BookAddViewModel bookAddViewModel)
        {
            var book = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);

            string bookFolderName = ReplaceInvalidCharacters(book.Title);
            string uploadPath = Path.Combine(env.WebRootPath, "img", "Books", bookFolderName, "cover");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string imgName = Path.GetFileName(bookAddViewModel.CoverImg.FileName);
            string imgPath = Path.Combine(uploadPath , imgName);
            using (FileStream stream = new FileStream(imgPath, FileMode.Create))
            {
                await animeStockDbContext.Pictures.AddAsync(new Picture()
                {
                    BookId = bookId,
                    Path = $"/img/Books/{bookFolderName}/cover/{imgName}"
                });
                await animeStockDbContext.SaveChangesAsync();
                await bookAddViewModel.CoverImg.CopyToAsync(stream);
            }
        }

        private string ReplaceInvalidCharacters(string input)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars()
                                    .Concat(Path.GetInvalidPathChars())
                                    .Distinct()
                                    .ToArray();

            foreach (char c in invalidChars)
            {
                input = input.Replace(c, ' ');
            }

            return input;
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync(Pager pager)
        {
            IEnumerable<BookViewModel> books = await animeStockDbContext.Books
                .OrderBy(b => b.IsDeleted)
                .Select(x => new BookViewModel()
            {
                Title = x.Title,
                PictureUrl = x.Pictures.FirstOrDefault(p => !p.IsDeleted && p.Path.Contains("cover")).Path,
                IsDeleted = x.IsDeleted,
                Id = x.Id,
            })
                .Skip((pager.CurrentPage - 1) * pager.PageSize)
                .Take(pager.PageSize)
                .ToArrayAsync();

            return books;
        }

        public async Task<int> GetAllBooksCountAsync()
        {
            var bookCount = await animeStockDbContext.Books.CountAsync();
            return bookCount;
        }
    }
}
