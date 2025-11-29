namespace GraphQl_BlogAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
