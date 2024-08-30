namespace InstagramClone.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; } = new User();

        // Додайте цю властивість
        public string ImageUrl { get; set; } = string.Empty;
    }
}
