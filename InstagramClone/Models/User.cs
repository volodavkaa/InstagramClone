using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstagramClone.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Властивості для підписників і підписок
        public ICollection<User> Followers { get; set; }
        public ICollection<User> Following { get; set; }

        // Зв’язок з постами
        public ICollection<Post> Posts { get; set; }

        // Підрахунок підписників і підписок
        public int FollowersCount => Followers?.Count ?? 0;
        public int FollowingCount => Following?.Count ?? 0;
    }
}
