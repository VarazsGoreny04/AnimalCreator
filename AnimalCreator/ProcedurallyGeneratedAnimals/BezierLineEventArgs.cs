namespace ProcedurallyGeneratedAnimals;

/// <summary>
/// Describes parameters of a Bézier line.
/// </summary>
public class BezierLineEventArgs
{
	protected Point<double>[] points;
	protected Transform[] transformations;
	protected Color color;

	/// <returns>The points of the line.</returns>
	public Point<double>[] Points => points;

	/// <returns>The transformations of the line.</returns>
	public Transform[] Transformations => transformations;

	/// <returns>The color of the line.</returns>
	public Color Color => color;

	/// <summary>
	/// Creates an BezierLineEventArgs object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="transformations">The transformations of the line.</param>
	/// <param name="color">The color of the line.</param>
	public BezierLineEventArgs(Point<double>[] points, Transform[] transformations, Color color)
	{
		this.points = points;
		this.transformations = transformations;
		this.color = color;
	}

	/// <summary>
	/// Creates an BezierLineEventArgs object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="transformations">The transformations of the line.</param>
	public BezierLineEventArgs(Point<double>[] points, Transform[] transformations) : this(points, transformations, new Color(0, 0, 0, 0)) { }
}