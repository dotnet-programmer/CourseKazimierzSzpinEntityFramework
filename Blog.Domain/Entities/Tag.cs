namespace Blog.Domain.Entities;

public class Tag
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Url { get; set; }

	public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
}