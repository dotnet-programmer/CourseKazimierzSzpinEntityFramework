using System.Windows;
using Blog.Desktop.ViewModels;

namespace Blog.Desktop.Views;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		DataContext = new MainViewModel();
	}
}