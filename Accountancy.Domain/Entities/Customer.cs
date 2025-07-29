namespace Accountancy.Domain.Entities;

public class Customer
{
	public int CustomerId { get; set; }
	public string? Name { get; set; }
	public string Nip { get; set; } = default!;
	public string? PhoneNumber { get; set; }
	public string? Email { get; set; }
	public bool IsDeleted { get; set; }

	public Address? Address { get; set; }
	public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
}