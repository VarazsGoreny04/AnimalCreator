using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes a pair of fins of a creature.
/// </summary>
internal class SideFin : BodyPart
{
	protected Point<int> dimensions;
	protected double angle;

	/// <returns>The length of the fin.</returns>
	public int Length => dimensions.Y;

	/// <returns>The width of the fin.</returns>
	public int Width => dimensions.X;

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
		dimensions = new Point<int>(width, length);
		this.angle = angle;
	}

	/// <summary>
	/// Draws this fin instance.
	/// </summary>
	public override List<ShapeData> Draw()
	{
		Point<double> front = Segment.GetFrontVector(segment);
		double frontAngle = Point.AngleOfVector(Point.NormalRight(front));

		Point<double> originOne = Point.Add(segment.Origin, Point.NormalLeft(front));
		Point<double> originTwo = Point.Add(segment.Origin, Point.NormalRight(front));

		return [
			new EllipseData(dimensions, originOne, frontAngle - angle, color),
			new EllipseData(dimensions, originTwo, frontAngle + angle, color)
		];
	}
}
