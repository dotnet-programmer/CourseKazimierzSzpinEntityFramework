using System;
using System.Windows;
using Blog.Desktop.ViewModels;

namespace Blog.Desktop.Views;

public partial class CategoryView : Window, IDisposable
{
	private readonly CategoryViewModel _viewModel;
	public CategoryView()
	{
		InitializeComponent();
		_viewModel = new CategoryViewModel();
		DataContext = _viewModel;
	}

	public void Dispose()
		=> _viewModel.Dispose();
}