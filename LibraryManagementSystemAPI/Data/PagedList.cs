namespace LibraryManagementSystemAPI.Data;

public sealed class PagedList<T> : List<T>
{
    public PagedList(T data, int pageSize, int pageNumber, int totalCount)
    {
        Data = data;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public bool HasNext => PageNumber <= TotalPages;
    public bool HasPrevious => PageNumber > 1;
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public T Data { get; }

}