namespace Blog.Domain.Entities;

public class Category
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Url { get; set; }
	public string Description { get; set; }
    public bool? IsDeleted { get; set; }

    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
}