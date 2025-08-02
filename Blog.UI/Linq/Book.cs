namespace Blog.UI.Linq;

internal class Book
{
	public int Id { get; set; }
	public string Title { get; set; } = default!;
	public decimal Price { get; set; }
	public int AuthorId { get; set; }
}