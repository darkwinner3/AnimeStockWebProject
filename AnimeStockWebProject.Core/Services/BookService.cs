﻿using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Book.Enum;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Core.Services
{
    public class BookService : IBookService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public BookService( AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }

        public async Task<AllBooksSortedDataModel> GetAllBooksSortedDataModelAsync(Guid? userId, BookQueryViewModel bookQueryViewModel)
        {
            IQueryable<Book> books = animeStockDbContext.Books.Where(b => !b.IsDeleted).AsQueryable();
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
                    ReleaseDate = b.ReleaseDate,
                    PrintType = b.PrintType.ToString(),
                    Price = b.Price,
                    PicturePath = b.Pictures.FirstOrDefault(p => !p.IsDeleted).Path,
                    IsFavorite = b.FavoriteProducts.Any(fp => fp.BookId == b.Id && fp.UserId == userId)
                }).ToArrayAsync();

            return new AllBooksSortedDataModel()
            {
                Books = allBooks
            };
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
                    books = books.OrderByDescending(b => b.ReleaseDate);
                    break;
                case BookSortEnum.ByOldestBook:
                    books = books.OrderBy(b => b.ReleaseDate);
                    break;
                case BookSortEnum.ByAToZ:
                    books = books.OrderBy(b => b.Title);
                    break;
                case BookSortEnum.ByZtoA:
                    books = books.OrderByDescending(b => b.Title);
                    break;
                default:
                    // By default, sort by newest
                    books = books.OrderByDescending(b => b.ReleaseDate);
                    break;
            }

            if (bookQueryViewModel.SelectedTagIds.Any())
            {
                foreach (var selectedTagId in bookQueryViewModel.SelectedTagIds)
                {
                    books = books.Where(b => b.BookTags.Any(bt => bt.TagId == selectedTagId));
                }
            }

            return books;
        }
    }
}