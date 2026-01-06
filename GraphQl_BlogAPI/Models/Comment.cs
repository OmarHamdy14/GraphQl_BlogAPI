namespace GraphQl_BlogAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
