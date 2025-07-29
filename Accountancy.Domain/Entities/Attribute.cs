namespace Accountancy.Domain.Entities;

public class Attribute
{
	public int AttributeId { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }

	public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}