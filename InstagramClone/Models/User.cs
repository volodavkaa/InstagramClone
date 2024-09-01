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
    public byte[]? ProfilePicture { get; set; }
    public string ProfileHeading { get; set; } = string.Empty;  // Заголовок (до 20 символів)
    public string ProfileBio { get; set; } = string.Empty;     // Опис (до 800 символів)
    public ICollection<UserFollower> Followers { get; set; }
    public ICollection<UserFollower> Following { get; set; }
    
    public ICollection<Post> Posts { get; set; } = new List<Post>();   
    public int FollowersCount => Followers?.Count ?? 0;
    public int FollowingCount => Following?.Count ?? 0;
}
