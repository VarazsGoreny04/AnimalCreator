using System.Windows.Media;

namespace AnimalCreator.WPF.ViewModel;

public class PathData : ShapeData
{
	private readonly Geometry pathFigures;
	private readonly Brush fill;

	public Geometry PathFigures => pathFigures;
	public Brush Fill => fill;

	public PathData(Geometry pathFigures, Transform transformations, Brush fill) : base(transformations)
	{
		this.pathFigures = pathFigures;
		this.fill = fill;
	}
}