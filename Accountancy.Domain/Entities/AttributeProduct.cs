namespace Accountancy.Domain.Entities;

public class AttributeProduct
{
	public int AttributeId { get; set; }
	public Attribute Attribute { get; set; }

	public int ProductId { get; set; }
	public Product Product { get; set; }
}