using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace AnimalCreator.WPF.ViewModel;

public abstract class ShapeData : INotifyPropertyChanged
{
	private readonly Transform transformations;

	public Transform Transformations => transformations;

	public event PropertyChangedEventHandler? PropertyChanged;

	public ShapeData(Transform transformations)
	{
		this.transformations = transformations;
	}

	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}