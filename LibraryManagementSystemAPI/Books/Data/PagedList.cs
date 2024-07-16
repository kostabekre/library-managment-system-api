namespace LibraryManagementSystemAPI;

public sealed class PagedList<T> : List<T>
{
    public PagedList(T data, int pageSize, int pageNumber, int totalCount, int totalPages)
    {
        Data = data;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        TotalPages = totalPages;
    }

    public bool HasNext => PageNumber <= TotalPages;
    public bool HasPrevious => PageNumber > 1;
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public T Data { get; }

}