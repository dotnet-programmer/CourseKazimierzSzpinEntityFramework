namespace Blog.Domain.Entities;

public class PostTag
{
	public DateTime CreatedDate { get; set; }

	public int PostId { get; set; }
	public Post Post { get; set; }

	public int TagId { get; set; }
	public Tag Tag { get; set; }
}