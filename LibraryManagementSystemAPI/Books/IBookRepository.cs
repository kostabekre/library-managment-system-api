using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Data;
using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Books;

public interface IBookRepository
{
    Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfoAsync();
    Task<PagedList<BookShortInfo>> GetBooksShortInfoAsync(BookParameters parameters);
    Task<int> CreateBookAsync(BookCreateDto model);
    Task<int> CreateBookWithCoverAsync(BookWithCoverCreateDto model);
    Task<BookInfo?> GetBookAsync(int id);
    Task<bool> RemoveBookAsync(int id);
    Task<bool> UpdateBookAsync(int id, BookUpdateDto bookDTO);
    Task<BookCoverDTO?> GetCoverAsync(int id);
    Task<bool> UpdateCoverAsync(int id, IFormFile file);
    Task<bool> UpdateBookRatingAsync(int id, int rating);
    Task<bool> UpdateBookAmountAsync(int id, int amount);
}