using Accountancy.Domain.Enums;

namespace Accountancy.Domain.Entities;

public class Invoice
{
	public int InvoiceId { get; set; }
	public int Number { get; set; }
	public int Year { get; set; }
	public byte Month { get; set; }
	public InvoiceType Type { get; set; }
	public DateTime CreatedDate { get; set; }
	public decimal TotalPrice { get; set; }
	public bool IsPaid { get; set; }

	public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<InvoicePosition> InvoicePositions { get; set; } = new HashSet<InvoicePosition>();
}