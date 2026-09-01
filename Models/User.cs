using System.ComponentModel.DataAnnotations;

namespace Buck2bar.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "User";

    public bool IsActive { get; set; } = true;

    //proprety surname string
    [Required]
    [StringLength(100)]
    public string Surname { get; set; } = string.Empty; 
}
