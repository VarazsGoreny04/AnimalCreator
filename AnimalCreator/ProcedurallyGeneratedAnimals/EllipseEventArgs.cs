namespace ProcedurallyGeneratedAnimals;

/// <summary>
/// Describes parameters of an ellipse.
/// </summary>
public class EllipseEventArgs
{
	protected Point<int> dimensions;
	protected Transform[] transforms;
	protected Color color;

	/// <returns>The dimensions of the ellipse.</returns>
	public Point<int> Dimensions => dimensions;

	/// <returns>The transformations of the ellipse.</returns>
	public Transform[] Transforms => transforms;

	/// <returns>The color of the ellipse.</returns>
	public Color Color => color;

	/// <summary>
	/// Creates an EllipseEventArgs object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="transforms">The transformations of the ellipse.</param>
	/// <param name="color">The color of the ellipse.</param>
	public EllipseEventArgs(Point<int> dimensions, Transform[] transforms, Color color)
	{
		this.dimensions = dimensions;
		this.transforms = transforms;
		this.color = color;
	}

	/// <summary>
	/// Creates an EllipseEventArgs object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="transforms">The transformations of the ellipse.</param>
	public EllipseEventArgs(Point<int> dimensions, Transform[] transforms) : this(dimensions, transforms, new Color(0, 0, 0, 0)) { }
}