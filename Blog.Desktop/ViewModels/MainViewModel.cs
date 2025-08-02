using System.Windows.Input;
using Blog.Desktop.Views;
using Prism.Commands;

namespace Blog.Desktop.ViewModels;

public class MainViewModel : BaseViewModel
{
	public MainViewModel()
		=> CreateCategoryCommand = new DelegateCommand(CreateCategory);

	public ICommand CreateCategoryCommand { get; }

	private void CreateCategory()
	{
		using (var categoryView = new CategoryView())
		categoryView.ShowDialog();
	}
}