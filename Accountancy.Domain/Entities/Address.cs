namespace Accountancy.Domain.Entities;

public class Address
{
	public int AddressId { get; set; }
	public string State { get; set; }
	public string City { get; set; }
	public string Street { get; set; }
	public string PostalCode { get; set; }

	public int CustomerId { get; set; }
	public Customer Customer { get; set; }
}