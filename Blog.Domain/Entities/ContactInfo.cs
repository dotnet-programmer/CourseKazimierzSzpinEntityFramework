namespace Blog.Domain.Entities;

public class ContactInfo
{
	public int Id { get; set; }
	public string Email { get; set; }

	public int UserId { get; set; }
	public User User { get; set; }
}