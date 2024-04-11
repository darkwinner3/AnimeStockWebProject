using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Areas.Admin.Models.Pictures;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.Pager;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
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
                Illustrator = WebUtility.HtmlEncode(bookAddViewModel.Illustrator),
                Pages = bookAddViewModel.Pages,
                Price = bookAddViewModel.Price,
                Publisher = WebUtility.HtmlEncode(bookAddViewModel.Publisher),
                Quantity = bookAddViewModel.Quantity,
                Title = WebUtility.HtmlEncode(bookAddViewModel.Title),
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
            
            var book = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);
            var fileName = bookAddViewModel.BookFile.FileName;
            var currentBookTitle = book.Title;
            var bookFile = bookAddViewModel.BookFile;

            await BookFile(book, fileName, currentBookTitle, bookFile);
            await animeStockDbContext.SaveChangesAsync();
        }
        

        public async Task CreateBookPicturesAsync(int bookId, BookAddViewModel bookAddViewModel)
        {
            var book = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);
            
            var bookTitle = book.Title;
            var pictures = bookAddViewModel.Pictures;

            await CreateBookPicturesAsync(bookId, bookTitle, pictures);
            
            await animeStockDbContext.SaveChangesAsync();
        }

       

        public async Task CreateCoverImgAsync(int bookId, BookAddViewModel bookAddViewModel)
        {
            var book = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);

            var bookTitle = book.Title;
            var fileName = bookAddViewModel.CoverImg.FileName;
            var coverImgFile = bookAddViewModel.CoverImg;

            await CreateCoverImageAsync(bookId, bookTitle, fileName, coverImgFile);
            animeStockDbContext.SaveChanges();
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
                BookTypeId = x.BookTypeId,
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

        public async Task<BookEditViewModel> GetBookToEditAsync(int bookId)
        {
            var bookToEdit = await animeStockDbContext.Books
                .Where(b => b.Id == bookId)
                .Select(b => new BookEditViewModel()
                {
                    Author = b.Author,
                    Description = b.Description,
                    FilePath = b.FilePath,
                    ReleaseDate = b.ReleaseDate,
                    Id = b.Id,
                    Illustrator = b.Illustrator,
                    Pages = b.Pages,
                    Price = b.Price,
                    PrintType = b.PrintType,
                    Publisher = b.Publisher,
                    Title = b.Title,
                    Quantity = b.Quantity,
                    CoverImg = b.Pictures.Where(p => p.Path.Contains("cover")).Select(p => new PictureAdminViewModel()
                    {
                        Id = p.Id,
                        IsDeleted = p.IsDeleted,
                        Path = p.Path,
                    }).ToArray(),
                    Pictures = b.Pictures.Where(p => !p.Path.Contains("cover")).Select(p => new PictureAdminViewModel()
                    {
                        Id = p.Id,
                        IsDeleted = p.IsDeleted,
                        Path = p.Path
                    }).ToArray(),
                    currentTags = b.BookTags.Where(bt => !bt.IsDeleted && !bt.Tag.IsDeleted).Select(bt => new TagViewModel()
                    {
                        Id = bt.TagId,
                        IsDeleted= bt.Tag.IsDeleted,
                        Name = bt.Tag.Name,
                    })
                })
                .FirstAsync();

            return bookToEdit;
        }

        public async Task EditBookByIdAsync(int bookId, BookEditViewModel bookEditViewModel)
        {
            Book bookToEdit = await animeStockDbContext.Books
                .Include(b => b.BookTags)
                .Include(b => b.Pictures)
                .FirstAsync(b => b.Id == bookId);
            bookToEdit.Title = WebUtility.HtmlEncode(bookEditViewModel.Title);
            bookToEdit.Quantity = bookEditViewModel.Quantity;
            bookToEdit.Price = bookEditViewModel.Price;
            bookToEdit.Author = WebUtility.HtmlEncode(bookEditViewModel.Author);
            bookToEdit.Description = WebUtility.HtmlEncode(bookEditViewModel.Description);
            bookToEdit.Illustrator = WebUtility.HtmlEncode(bookEditViewModel.Illustrator);
            bookToEdit.Pages = bookEditViewModel.Pages;
            bookToEdit.ReleaseDate = bookEditViewModel.ReleaseDate;
            bookToEdit.Publisher = WebUtility.HtmlEncode(bookEditViewModel.Publisher);
            bookToEdit.PrintType = bookEditViewModel.PrintType;
            bookToEdit.BookTypeId = bookEditViewModel.BookTypeId;

            var tagsToRemove = bookToEdit.BookTags.Where(bt => !bookEditViewModel.SelectedBookTagIds.Contains(bt.TagId)).ToArray();
            animeStockDbContext.BookTags.RemoveRange(tagsToRemove);
            await animeStockDbContext.SaveChangesAsync();
            if (bookEditViewModel.SelectedBookTagIds.Any())
            {
                var tagsToAdd = bookEditViewModel.SelectedBookTagIds.Where(id => !bookToEdit.BookTags.Any(bt => bt.TagId == id));
                foreach (var selectedTag in tagsToAdd)
                {
                    await animeStockDbContext.BookTags.AddAsync(new BooksTags() { BookId = bookId, TagId = selectedTag });
                    await animeStockDbContext.SaveChangesAsync();
                }
            }
            if (bookEditViewModel.NewCoverImg != null
                && !bookToEdit.Pictures.Any(p => p.Path.Contains("cover") && !p.IsDeleted))
            {
                await CreateCoverImageAsync(bookId, bookToEdit.Title, bookEditViewModel.NewCoverImg.FileName, bookEditViewModel.NewCoverImg);
                await animeStockDbContext.SaveChangesAsync();
            }
            int newPicturesCount = 0;
            if (bookEditViewModel.NewPictures != null)
            {
                newPicturesCount = bookEditViewModel.NewPictures.Count();
            }
            int bookImageCount = newPicturesCount + bookToEdit.Pictures.Where(p => !p.IsDeleted && !p.Path.Contains("cover")).Count();
            if (bookImageCount <= 5 && bookEditViewModel.NewPictures != null)
            {
                await CreateBookPicturesAsync(bookId, bookToEdit.Title, bookEditViewModel.NewPictures);
                await animeStockDbContext.SaveChangesAsync();
            }
            if (bookEditViewModel.BookFile != null)
            {
                string? oldFileFolderName = bookToEdit.FilePath;
                if (oldFileFolderName != null)
                {
                    if (File.Exists(oldFileFolderName))
                    {
                        File.Delete(oldFileFolderName);
                    }
                    bookToEdit.FilePath = null;
                }
                
                var fileName = bookEditViewModel.BookFile?.FileName;
                var bookFile = bookEditViewModel.BookFile;

                await BookFile(bookToEdit, fileName, bookToEdit.Title, bookFile);
                await animeStockDbContext.SaveChangesAsync();
            }
            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task DeleteBookByIdAsync(int bookId)
        {
            var bookToDelete = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);
            bookToDelete.IsDeleted = true;
            await animeStockDbContext.SaveChangesAsync();
        }


        public async Task RecoverBookByIdAsync(int bookId)
        {
            var bookToRecover = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookId);
            bookToRecover.IsDeleted = false;
            await animeStockDbContext.SaveChangesAsync();
        }

        private string ExtractCoreTitle(string fullTitle)
        {
            string[] separators = { ",", "(" }; // Splitting by comma or (
            string[] parts = fullTitle.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return parts[0].Trim(); // Taking the first part as core title
        }

        private async Task BookFile(Book book, string? fileName, string currentBookTitle, IFormFile? bookFile)
        {
            bool mangaBookFolderExists = false;
            bool bookFolderExists = false;
            var fileType = Path.GetExtension(fileName);
            string directoryPath = Path.Combine(env.WebRootPath, "Books");

            string clearBookTitle = ReplaceInvalidCharacters(currentBookTitle);
            string coreTitle = ExtractCoreTitle(clearBookTitle);
            if (fileType == ".cbz")
            {
                coreTitle += " (manga)";
            }
            string uploadPath = Path.Combine(directoryPath, coreTitle);

            string[] folderPaths = Directory.GetDirectories(directoryPath);
            foreach (var folderPath in folderPaths)
            {
                string folderName = Path.GetFileName(folderPath);
                if (clearBookTitle.ToLower().Contains(folderName.ToLower()) && fileType == ".cbz" && folderName.ToLower().Contains("manga"))
                {
                    string filePath = Path.Combine(directoryPath, folderName, fileName);
                    string bookFilePath = Path.Combine("Books", folderName, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        book.FilePath = bookFilePath;
                        await animeStockDbContext.SaveChangesAsync();
                        await bookFile.CopyToAsync(stream);
                    }
                    mangaBookFolderExists = true;
                    break;
                }
                else if (clearBookTitle.ToLower().Contains(folderName.ToLower()) && fileType == ".pdf")
                {
                    string filePath = Path.Combine(directoryPath, folderName, fileName);
                    string bookFilePath = Path.Combine("Books", folderName, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        book.FilePath = bookFilePath;
                        await animeStockDbContext.SaveChangesAsync();
                        await bookFile.CopyToAsync(stream);
                    }
                    bookFolderExists = true;
                    break;
                }
            }
            if (!bookFolderExists && fileType == ".pdf")
            {
                Directory.CreateDirectory(uploadPath);

                string filePath = Path.Combine(directoryPath, coreTitle, fileName);
                string bookFilePath = Path.Combine("Books", coreTitle, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    book.FilePath = bookFilePath;
                    await animeStockDbContext.SaveChangesAsync();
                    await bookFile.CopyToAsync(stream);
                }
            }
            if (!mangaBookFolderExists && fileType == ".cbz")
            {
                Directory.CreateDirectory(uploadPath);

                string filePath = Path.Combine(directoryPath, coreTitle, fileName);
                string bookFilePath = Path.Combine("Books", coreTitle, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    book.FilePath = bookFilePath;
                    await animeStockDbContext.SaveChangesAsync();
                    await bookFile.CopyToAsync(stream);
                }
            }
        }
        private async Task CreateCoverImageAsync(int bookId, string bookTitle, string fileName, IFormFile coverImgFile)
        {
            string bookFolderName = ReplaceInvalidCharacters(bookTitle);
            string uploadPath = Path.Combine(env.WebRootPath, "img", "Books", bookFolderName, "cover");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string imgName = Path.GetFileName(fileName);
            string imgPath = Path.Combine(uploadPath, imgName);
            using (FileStream stream = new FileStream(imgPath, FileMode.Create))
            {
                await animeStockDbContext.Pictures.AddAsync(new Picture()
                {
                    BookId = bookId,
                    Path = $"/img/Books/{bookFolderName}/cover/{imgName}"
                });
                await animeStockDbContext.SaveChangesAsync();
                await coverImgFile.CopyToAsync(stream);
            }
        }

        private async Task CreateBookPicturesAsync(int bookId, string bookTitle, IFormFileCollection pictures)
        {
            string bookFolderName = ReplaceInvalidCharacters(bookTitle);
            string uploadPath = Path.Combine(env.WebRootPath, "img", "Books", bookFolderName);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            foreach (var img in pictures!)
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

        private async Task DeletePictures(Picture[] pictures)
        {
            string? pictureFolderName = Path.GetDirectoryName(pictures[0].Path);
            string? directory = Path.GetDirectoryName(pictureFolderName);
            string deletePath;
            if (directory != "img")
            {
                deletePath = Path.Join(env.WebRootPath, directory);
            }
            else
            {
                deletePath = Path.Join(env.WebRootPath, pictureFolderName);
            }

            if (Directory.Exists(deletePath))
            {
                Directory.Delete(deletePath, true);
            }
            foreach (var picture in pictures)
            {
                animeStockDbContext.Remove(picture);
            }

            await animeStockDbContext.SaveChangesAsync();
        }


        //Deleting all book information every 3 days
        public async Task DeleteBooksJobAsync()
        {
            var booksToDelete = await animeStockDbContext.Books
                .Include(b => b.Pictures)
                .Include(b => b.BookTags)
                .Include(b => b.FavoriteProducts)
                .Include(b => b.Comments)
                .Where(b => b.IsDeleted)
                .ToArrayAsync();
            
            if (booksToDelete.Length > 0)
            {
                foreach (var book in booksToDelete)
                {
                    var tagsToRemove = await animeStockDbContext.BookTags.Where(b => b.BookId == book.Id).ToArrayAsync();
                    if (tagsToRemove.Any())
                    {
                        animeStockDbContext.RemoveRange(tagsToRemove);
                        await animeStockDbContext.SaveChangesAsync();
                    }
                    var picturesToRemove = await animeStockDbContext.Pictures.Where(p => p.BookId == book.Id).ToArrayAsync();
                    if (picturesToRemove.Any())
                    {
                        await DeletePictures(picturesToRemove);
                    }
                    var favoritesToRemove = await animeStockDbContext.FavoriteProducts.Where(b => b.BookId == book.Id).ToArrayAsync();
                    if (favoritesToRemove.Any())
                    {
                        animeStockDbContext.RemoveRange(favoritesToRemove);
                        await animeStockDbContext.SaveChangesAsync();
                    }
                    var commentsToRemove = await animeStockDbContext.Comments.Where(c => c.BookId == book.Id).ToArrayAsync();
                    if (commentsToRemove.Any())
                    {
                        animeStockDbContext.RemoveRange(commentsToRemove);
                        await animeStockDbContext.SaveChangesAsync();
                    }
                    
                    if (!string.IsNullOrWhiteSpace(book.FilePath))
                    {
                        string? FilePath = Path.Join(env.WebRootPath, book.FilePath);
                        File.Delete(FilePath);
                    }
                    await animeStockDbContext.SaveChangesAsync();
                    animeStockDbContext.Remove(book);
                }
            }
            
            await animeStockDbContext.SaveChangesAsync();
        }
    }

}
