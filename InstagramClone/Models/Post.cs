namespace InstagramClone.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } 
    }

}
