namespace LibraryManagementSystemAPI.Models;

public record Error(int Code, IEnumerable<string> Messages)
{
    public static Error BadRequest(IEnumerable<string> messages) => new(400, messages);
    public static Error NotFound() => new(404, new[] { "Not Found" });
}

