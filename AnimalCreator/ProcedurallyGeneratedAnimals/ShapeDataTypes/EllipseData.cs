namespace ProcedurallyGeneratedAnimals.ShapeDataTypes;

/// <summary>
/// Describes an ellipse.
/// </summary>
public class EllipseData : ShapeData
{
	protected Point<int> dimensions;
	protected Point<double> position;
	protected double? angle;

	/// <returns>The dimensions of the ellipse.</returns>
	public Point<int> Dimensions => dimensions;

	/// <returns>The width of the ellipse.</returns>
	public int Width => dimensions.X;

	/// <returns>The height of the ellipse.</returns>
	public int Height => dimensions.Y;

	/// <returns>The position of the ellipse.</returns>
	public Point<double> Position => position;

	/// <returns>The angle of the ellipse.</returns>
	public double? Angle => angle;

	/// <summary>
	/// Creates an <see cref="EllipseData"/> object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="position">The position of the ellipse.</param>
	/// <param name="angle">The angle of the ellipse.</param>
	public EllipseData(Point<int> dimensions, Point<double> position, double? angle = null, Color? color = null) : base(color)
	{
		this.dimensions = dimensions;
		this.position = position;
		this.angle = angle;
	}
}