using AnimalCreator.WPF.ViewModel;
using System.Windows;

namespace AnimalCreator.WPF;

public partial class App : Application
{
	#region Fields

	private MainWindow mainWindow = null!;
	private AnimalCreatorViewModel viewModel = null!;

	#endregion

	#region Constructors

	public App()
	{
		Startup += new StartupEventHandler(AppStartUp);
	}

	#endregion

	#region Private methods

	private void AppStartUp(object? sender, StartupEventArgs e)
	{
		viewModel = new AnimalCreatorViewModel();

		mainWindow = new MainWindow() { DataContext = viewModel	};
		mainWindow.Show();

		mainWindow.DrawArea.MouseMove += new System.Windows.Input.MouseEventHandler(viewModel.OnMouseMove);
		mainWindow.Closing += new System.ComponentModel.CancelEventHandler(viewModel.OnClosingWindow);
	}

	#endregion
}