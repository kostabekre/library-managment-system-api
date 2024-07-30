namespace LibraryManagementSystemAPI.Models;

public record Error(int Code, IEnumerable<string> Messages);