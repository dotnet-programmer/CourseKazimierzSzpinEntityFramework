namespace Accountancy.Domain.Entities.Views;

public class CustomerAddressView
{
	public int CustomerId { get; set; }
	public string? Name { get; set; }
	public string? Nip { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Email { get; set; }
	public bool IsDeleted { get; set; }
	public string? Address { get; set; }
}