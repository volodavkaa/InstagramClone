using InstagramClone.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    // Властивості для підписників і підписок
    public ICollection<User> Followers { get; set; } = new List<User>();
    public ICollection<User> Following { get; set; } = new List<User>();

    // Зв’язок з постами
    public ICollection<Post> Posts { get; set; } = new List<Post>();

    // Підрахунок підписників і підписок
    public int FollowersCount => Followers?.Count ?? 0;
    public int FollowingCount => Following?.Count ?? 0;
}
