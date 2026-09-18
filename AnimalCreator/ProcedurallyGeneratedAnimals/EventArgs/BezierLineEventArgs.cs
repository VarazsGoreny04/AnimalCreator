namespace ProcedurallyGeneratedAnimals.EventArgs;

/// <summary>
/// Describes parameters of a Bézier line.
/// </summary>
public class BezierLineEventArgs
{
	protected Point<double>[] points;
	protected Point<double>? position;
	protected double? angle;
	protected Color? color;

	/// <returns>The points of the line.</returns>
	public Point<double>[] Points => points;

	/// <returns>The origin position of the line.</returns>
	public Point<double>? Position => position;

	/// <returns>The angle of the line.</returns>
	public double? Angle => angle;

	/// <returns>The color of the line.</returns>
	public Color? Color => color;

	/// <summary>
	/// Creates an BezierLineEventArgs object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="position">The origin position of the line.</param>
	/// <param name="angle">The angle of the line.</param>
	/// <param name="color">The color of the line.</param>
	public BezierLineEventArgs(Point<double>[] points, Point<double>? position = null, double? angle = null, Color? color = null)
	{
		this.points = points;
		this.position = position;
		this.angle = angle;
		this.color = color;
	}
}