using Blog.Domain.Enums;

namespace Blog.Domain.Entities;

public class Post
{
	public int Id { get; set; }
	public string Title { get; set; } = default!;
	public string? Description { get; set; }
	public string? ShortDescription { get; set; }
	public string? Url { get; set; }
	public bool? Published { get; set; }
	public DateTime PostedOn { get; set; }
	public DateTime? Modified { get; set; }
	public PostType Type { get; set; }
	public int CategoryId { get; set; }
	public int UserId { get; set; }
	public int? ApprovedByUserId { get; set; }

	public Category? Category { get; set; }
	public User? User { get; set; }
	public User? ApprovedBy { get; set; }
	public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
}