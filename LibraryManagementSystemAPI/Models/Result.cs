namespace LibraryManagementSystemAPI.Models;

public class Result<T>
{
    private Result(T? data)
    {
        Data = data;
    }

    private Result(Error error)
    {
        IsFailure = true;
        Error = error;
    }
    public T? Data { get; init; }
    public bool IsFailure { get; init; }
    public Error? Error { get; init; }

    public static implicit operator Result<T>(T data) => new Result<T>(data);
    public static implicit operator Result<T>(Error error) => new Result<T>(error);
}