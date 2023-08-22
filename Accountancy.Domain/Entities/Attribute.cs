namespace Accountancy.Domain.Entities;

public class Attribute
{
	public int AttributeId { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }

	public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}