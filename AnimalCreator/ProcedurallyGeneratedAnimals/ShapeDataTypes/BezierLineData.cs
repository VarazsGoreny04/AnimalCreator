namespace ProcedurallyGeneratedAnimals.ShapeDataTypes;

/// <summary>
/// Describes a Bézier line.
/// </summary>
public class BezierLineData	: ShapeData
{
	protected Point<double>[] points;
	protected Point<double>? position;
	protected double? angle;

	/// <returns>The points of the line.</returns>
	public Point<double>[] Points => points;

	/// <returns>The origin position of the line.</returns>
	public Point<double>? Position => position;

	/// <returns>The angle of the line.</returns>
	public double? Angle => angle;

	/// <summary>
	/// Creates a <see cref="BezierLineData"/> object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="position">The origin position of the line.</param>
	/// <param name="angle">The angle of the line.</param>
	public BezierLineData(Point<double>[] points, Point<double>? position = null, double? angle = null, Color? color = null) : base(color)
	{
		this.points = points;
		this.position = position;
		this.angle = angle;
	}
}