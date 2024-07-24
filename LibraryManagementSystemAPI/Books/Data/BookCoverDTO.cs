using System.Net.Mime;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookCoverDTO
{
    public byte[] File { get; init; }
    public ContentDisposition CD { get; init; }
}