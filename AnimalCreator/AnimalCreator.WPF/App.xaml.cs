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

	public App() => Startup += new StartupEventHandler(AppStartUp);

	#endregion

	#region Private methods

	private void AppStartUp(object? sender, StartupEventArgs e)
	{
		viewModel = new AnimalCreatorViewModel();

		mainWindow = new MainWindow(viewModel);
		mainWindow.Show();
	}

	#endregion
}