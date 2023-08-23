namespace Blog.Domain.Entities;

public class User
{
	public int Id { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }

	// w tej relacji 1:1 User jest podmiotem głównym, a ContactInfo jest podmiotem zależnym
	// w takim przypadku w User wystarczy dodać właściwość nawigacyjną, a w ContactInfo dodatkowo klucz obcy
	public ContactInfo ContactInfo { get; set; }

	public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
	
	public ICollection<Post> PostsApproved { get; set; } = new HashSet<Post>();
}