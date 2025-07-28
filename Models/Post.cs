namespace Gossip.Models;

public class Post
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ProfileID { get; set; }

    public Profile Author { get; set; }
}