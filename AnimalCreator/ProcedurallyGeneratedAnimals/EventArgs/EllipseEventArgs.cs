namespace ProcedurallyGeneratedAnimals.EventArgs;

/// <summary>
/// Describes parameters of an ellipse.
/// </summary>
public class EllipseEventArgs
{
	protected Point<int> dimensions;
	protected Point<double> position;
	protected double? angle;
	protected Color? color;

	/// <returns>The dimensions of the ellipse.</returns>
	public Point<int> Dimensions => dimensions;

	/// <returns>The position of the ellipse.</returns>
	public Point<double> Position => position;

	/// <returns>The angle of the ellipse.</returns>
	public double? Angle => angle;

	/// <returns>The color of the ellipse.</returns>
	public Color? Color => color;

	/// <summary>
	/// Creates an EllipseEventArgs object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="position">The position of the ellipse.</param>
	/// <param name="angle">The angle of the ellipse.</param>
	/// <param name="color">The color of the ellipse.</param>
	public EllipseEventArgs(Point<int> dimensions, Point<double> position, double? angle = null, Color? color = null)
	{
		this.dimensions = dimensions;
		this.position = position;
		this.angle = angle;
		this.color = color;
	}
}