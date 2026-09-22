using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes a pair of fins of a creature.
/// </summary>
internal class SideFin : BodyPart
{
	protected Point<uint> dimensions;
	protected double angle;

	/// <returns>The width of the fin.</returns>
	public uint Width => dimensions.Y;

	/// <returns>The length of the fin.</returns>
	public uint Length => dimensions.X;

	/// <returns>The angle between the fin and the spine of the animal.</returns>
	public double Angle => angle;

	/// <summary>
	/// Creates a <see cref="SideFin"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="dimensions">The dimensions of the fin.</param>
	/// <param name="angle">The angle between the fin and the spine of the animal.</param>
	/// <param name="color">Color of the fin.</param>
	public SideFin(Segment segment, Render render, Point<uint> dimensions, double angle, Color color) : base(segment, render, color)
	{
		this.dimensions = dimensions;
		this.angle = angle;
	}

	/// <summary>
	/// Creates a <see cref="SideFin"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="length">The length of the fin.</param>
	/// <param name="width">The width of the fin.</param>
	/// <param name="angle">The angle between the fin and the spine of the animal.</param>
	/// <param name="color">Color of the fin.</param>
	public SideFin(Segment segment, Render render, uint width, uint length, double angle, Color color)
		: this(segment, render, new Point<uint>(length, width), angle, color) { }

	/// <summary>
	/// Draws this fin instance.
	/// </summary>
	public override List<ShapeData> Draw()
	{
		Point<double> front = Segment.GetFrontVector(segment);
		double frontAngle = Point.AngleOfVector(front);

		Point<double> center = new(-(double)Length / 2, 0);

		Point<double> originLeft = Point.Add(segment.Origin, Point.NormalLeft(front));
		Point<double> originRight = Point.Add(segment.Origin, Point.NormalRight(front));

		return [
			new EllipseData(dimensions, originLeft, frontAngle - angle, center, color),
			new EllipseData(dimensions, originRight, frontAngle + angle, center, color)
		];
	}
}