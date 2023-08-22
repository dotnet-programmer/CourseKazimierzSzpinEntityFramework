namespace Accountancy.Domain.Entities;

public class Product
{
	public int ProductId { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }

	public ICollection<InvoicePosition> InvoicePositions { get; set; } = new HashSet<InvoicePosition>();

	public ICollection<Attribute> Attributes { get; set; } = new HashSet<Attribute>();
}