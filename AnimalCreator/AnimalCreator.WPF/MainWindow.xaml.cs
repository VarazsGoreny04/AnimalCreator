using AnimalCreator.WPF.ViewModel;
using System.Windows;

namespace AnimalCreator.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private AnimalCreatorViewModel viewModel;

	public MainWindow(AnimalCreatorViewModel viewModel)
	{
		this.viewModel = viewModel;

		DataContext = this.viewModel;

		InitializeComponent();
	}
}