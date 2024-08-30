using System.Collections.Generic;


namespace InstagramClone.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public string ProfilePictureUrl { get; set; } = "/images/default-avatar.png"; // Вказуємо шлях до зображення за замовчуванням
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }

}
