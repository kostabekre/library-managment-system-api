using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemAPI.Models;

public class BookUpdateDTO
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public int[] GenresId { get; set; }
}