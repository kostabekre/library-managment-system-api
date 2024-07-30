using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Data;
using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Books;

public interface IBookRepository
{
    Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfo();
    Task<PagedList<IList<BookShortInfo>>> GetBooksShortInfo(BookParameters parameters);
    Task<Result<int>> CreateBook(BookCreateDto model);
    Task<Result<int>> CreateBookWithCover(BookWithCoverCreateDto model);
    Task<BookInfo?> GetBook(int id);
    Task<bool> RemoveBook(int id);
    Task<bool> UpdateBook(int id, BookUpdateDto bookDTO);
    Task<BookCoverDTO?> GetCover(int id);
    Task<Error?> UpdateCover(int id, IFormFile file);
    Task<Error?> UpdateBookRating(int id, int rating);
    Task<Error?> UpdateBookAmount(int id, int amount);
}