namespace Blog.Domain.Entities;

public class User
{
	public int Id { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }

	public ContactInfo ContactInfo { get; set; }

	public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
}