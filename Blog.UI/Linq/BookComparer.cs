using System.Diagnostics.CodeAnalysis;

namespace Blog.UI.Linq;

// klasa używana np jako argument wywołania Distinct()
internal class BookComparer : IEqualityComparer<Book>
{
	public bool Equals(Book? x, Book? y)
		=> x?.Id == y?.Id;

	public int GetHashCode([DisallowNull] Book obj)
		=> obj.Id.GetHashCode();
}