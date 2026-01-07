using GraphQl_BlogAPI.Eunms;

namespace GraphQl_BlogAPI.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public ReactionKind Type { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; } 
        public Post Post { get; set; }
    }
}
