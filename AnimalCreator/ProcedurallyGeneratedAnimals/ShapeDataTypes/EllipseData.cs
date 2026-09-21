namespace ProcedurallyGeneratedAnimals.ShapeDataTypes;

/// <summary>
/// Describes an ellipse.
/// </summary>
public class EllipseData : ShapeData
{
	protected Point<uint> dimensions;
	protected Point<double> position;
	protected (double, Point<double>?)? rotation;

	/// <returns>The dimensions of the ellipse.</returns>
	public Point<uint> Dimensions => dimensions;

	/// <returns>The width of the ellipse.</returns>
	public uint Width => dimensions.X;

	/// <returns>The height of the ellipse.</returns>
	public uint Height => dimensions.Y;

	/// <returns>The position of the ellipse.</returns>
	public Point<double> Position => position;

	/// <returns>The angle of the ellipse.</returns>
	public (double Angle, Point<double>? Center)? Rotation => rotation;

	/// <summary>
	/// Creates an <see cref="EllipseData"/> object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="position">The position of the ellipse.</param>
	/// <param name="angle">The angle of the ellipse.</param>
	/// <param name="center">The center of the rotation.</param>
	public EllipseData(Point<uint> dimensions, Point<double> position, double angle, Point<double>? center = null, Color? color = null) : base(color)
	{
		this.dimensions = dimensions;
		this.position = position;
		rotation = (angle, center);
	}

	/// <summary>
	/// Creates an <see cref="EllipseData"/> object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="position">The position of the ellipse.</param>
	/// <param name="angle">The angle of the ellipse.</param>
	public EllipseData(Point<uint> dimensions, Point<double> position, double? angle = null, Color? color = null) : base(color)
	{
		this.dimensions = dimensions;
		this.position = position;
		rotation = angle is double d ? (d, null) : null;
	}
}