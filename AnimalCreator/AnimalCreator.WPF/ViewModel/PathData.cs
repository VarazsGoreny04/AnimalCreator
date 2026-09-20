using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace AnimalCreator.WPF.ViewModel;

public class PathData : INotifyPropertyChanged
{
	private readonly Geometry geometry;
	private readonly Transform transformations;
	private readonly Brush fill;

	public Geometry Geometry => geometry;
	public Transform Transformations => transformations;
	public Brush Fill => fill;

	public event PropertyChangedEventHandler? PropertyChanged;

	public PathData(Geometry geometry, Transform transformations, Brush fill)
	{
		this.geometry = geometry;
		this.transformations = transformations;
		this.fill = fill;
	}

	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}