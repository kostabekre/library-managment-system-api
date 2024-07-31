namespace LibraryManagementSystemAPI.Models;

public class Result<T>
{
    public Result(T? data)
    {
        Data = data;
    }

    public Result(Error error)
    {
        IsFailure = true;
        Error = error;
    }
    public T? Data { get; init; }
    public bool IsFailure { get; init; }
    public Error? Error { get; init; }
}