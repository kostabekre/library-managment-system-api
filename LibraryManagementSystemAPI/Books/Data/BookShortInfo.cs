using LibraryManagementSystemAPI.Authors.Models;

namespace LibraryManagementSystemAPI.Books.Data;

public record BookShortInfo(int BookId, string Name, AuthorShortInfo Author);