namespace GraphQl_BlogAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
