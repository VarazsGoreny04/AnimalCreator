namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes a pair of fins of a creature.
/// </summary>
internal class SideFin : BodyPart
{
	protected int length;
	protected int width;
	protected double angle;

	/// <returns>The length of the fin.</returns>
	public int Length => length;

	/// <returns>The width of the fin.</returns>
	public int Width => width;

	/// <returns>The angle between the fin and the spine of the animal.</returns>
	public double Angle => angle;

	/// <summary>
	/// Creates a <see cref="SideFin"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="length">The length of the fin.</param>
	/// <param name="width">The width of the fin.</param>
	/// <param name="angle">The angle between the fin and the spine of the animal.</param>
	/// <param name="color">Color of the fin.</param>
	public SideFin(Segment segment, Render render, int length, int width, double angle, Color color) : base(segment, render, color)
	{
		this.length = length;
		this.width = width;
		this.angle = angle;
	}

	/// <summary>
	/// Draws an ellipse.
	/// </summary>
	/// <param name="position">The position of the ellipse.</param>
	/// <param name="angle">The angle of the ellipse.</param>
	/// <param name="width">The width of the ellipse.</param>
	/// <param name="length">The height of the ellipse.</param>
	public static void DrawEllipseByOrientation(Point<double> position, int width, int length, double angle, Color color)
	{
		Animal.OnDrawEllipse(new Point<int>(width, length / 2), [new Rotate(angle), new Translate(position)], color);
	}

	/// <summary>
	/// Draws this fin instance.
	/// </summary>
	public override void Draw()
	{
		Point<double> front = Segment.GetFrontVector(segment);
		double frontAngle = Point.AngleOfVector(Point.NormalRight(front));

		Point<double> originOne = Point.Add(segment.Origin, Point.NormalLeft(front));
		DrawEllipseByOrientation(originOne, width, length, frontAngle - angle, color);

		Point<double> originTwo = Point.Add(segment.Origin, Point.NormalRight(front));
		DrawEllipseByOrientation(originTwo, width, length, frontAngle + angle, color);
	}
}
