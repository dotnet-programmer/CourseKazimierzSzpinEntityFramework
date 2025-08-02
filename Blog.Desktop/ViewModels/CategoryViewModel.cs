using System;
using System.Windows;
using System.Windows.Input;
using Blog.DataLayer;
using Blog.Domain.Entities;
using Prism.Commands;

namespace Blog.Desktop.ViewModels;

public class CategoryViewModel : BaseViewModel, IDisposable
{
	private readonly AppDbContext _context;
	private Category _category;

	public CategoryViewModel()
	{
		_context = new AppDbContext();
		Category = new Category();

		SaveCommand = new DelegateCommand(Save);
		AddCommand = new DelegateCommand(Add);
		CancelCommand = new DelegateCommand<Window>(Cancel);
	}

	public ICommand SaveCommand { get; }
	public ICommand AddCommand { get; }
	public ICommand CancelCommand { get; }

	public Category Category
	{
		get => _category;
		set
		{
			_category = value;
			OnPropertyChanged();
		}
	}

	private async void Save()
	{
		await _context.SaveChangesAsync();
		Category = new Category();
	}

	private void Add()
	{
		_context.Categories.Add(Category);
		Category = new Category();
	}

	private void Cancel(Window window)
	{
		if (_context.ChangeTracker.HasChanges())
		{
			var result = MessageBox.Show("Wykryto nie zapisane zmiany. Czy na pewno chcesz zamknąć okno?", "Potwierdź zamknięcie okna", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.No)
			{
				return;
			}
		}

		window.Close();
	}

	public void Dispose()
		=> _context.Dispose();
}