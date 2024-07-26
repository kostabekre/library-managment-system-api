namespace LibraryManagementSystemAPI.Models;

public class Result<T>
{
    public T? Data { get; init; }
    public bool IsFailure { get; init; }
    public Error? Error { get; init; }
}