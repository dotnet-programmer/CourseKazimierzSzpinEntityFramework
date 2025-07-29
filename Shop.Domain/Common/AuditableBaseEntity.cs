namespace Shop.Domain.Common;

// dodanie klasy, której zadaniem jest przechowywanie informacji o tym, kiedy i przez kogo został utworzony lub zmodyfikowany dany rekord w bazie danych
// jeżeli potrzeba jest zapisywania tych informacji w bazie danych, to klasa ta powinna być dziedziczona przez inne klasy encji
public abstract class AuditableBaseEntity
{
	public DateTime Created { get; set; }
	public string CreatedBy { get; set; } = default!;
	public DateTime? LastModified { get; set; }
	public string? LastModifiedBy { get; set; }
}