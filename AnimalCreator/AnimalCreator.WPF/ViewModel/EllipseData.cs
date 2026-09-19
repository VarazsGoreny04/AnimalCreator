using System.Windows;
using System.Windows.Media;

namespace AnimalCreator.WPF.ViewModel;

public class EllipseData : ShapeData
{
	private readonly double width;
	private readonly double height;
	private readonly Thickness margin;
	private readonly Brush fill;

	public double Width => width;
	public double Height => height;
	public Thickness Margin => margin;
	public Brush Fill => fill;

	public EllipseData(double width, double height, Transform transformations, Brush fill) : base(transformations)
	{
		this.width = width;
		this.height = height;
		this.fill = fill;

		margin = new Thickness(-width / 2, -height / 2, 0, 0);
	}
}