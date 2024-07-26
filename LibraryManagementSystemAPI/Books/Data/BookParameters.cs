namespace LibraryManagementSystemAPI.Books.Data;

public class BookParameters
{
    private int _pageSize;
    private int _pageNumber;

    public int PageNumber
    {
        get => _pageNumber;
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            
            _pageNumber = value;
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 0)
            {
                value = 1;
            }

            _pageSize = value;
        }
    }
}