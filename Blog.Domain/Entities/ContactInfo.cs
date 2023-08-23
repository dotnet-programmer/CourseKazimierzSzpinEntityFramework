namespace Blog.Domain.Entities;

public class ContactInfo
{
	public int Id { get; set; }
	public string Email { get; set; }

	// w tej relacji 1:1 User jest podmiotem głównym, a ContactInfo jest podmiotem zależnym
	// w takim przypadku w User wystarczy dodać właściwość nawigacyjną, a w ContactInfo dodatkowo klucz obcy
	public int UserId { get; set; }
	public User User { get; set; }
}