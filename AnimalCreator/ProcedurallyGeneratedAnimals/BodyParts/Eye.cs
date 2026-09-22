using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes one pair of eyes of a creature.
/// </summary>
internal class Eye : BodyPart
{
	protected double radianToFront;
	protected uint distanceToOrigin;
	protected uint radius;

	/// <returns>The angle of the eye from the front vector of the segment.</returns>
	public double RadianToFront => radianToFront;

	/// <returns>The distance of the eye from the center of the segment.</returns>
	public uint DistanceToOrigin => distanceToOrigin;

	/// <returns>Radius of the eye.</returns>
	public uint Radius => radius;

	/// <summary>
	/// Creates an <see cref="Eye"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="angleToFront">The angle of the eye from the front vector of the segment.</param>
	/// <param name="distanceToOrigin">The distance of the eye from the center of the segment.</param>
	/// <param name="radius">Radius of the eye.</param>
	/// <param name="color">Color of the eye.</param>
	public Eye(Segment segment, Render render, double angleToFront, uint distanceToOrigin, uint radius, Color color) : base(segment, render, color)
	{
		radianToFront = double.DegreesToRadians(angleToFront);
		this.distanceToOrigin = distanceToOrigin;
		this.radius = radius;
	}

	/// <summary>
	/// Draws this eye instance.
	/// </summary>
	public override List<ShapeData> Draw()
	{
		Point<double> frontScaled = Point.Scale(Segment.GetFrontVector(segment), distanceToOrigin);

		Point<double> eyePoint = Point.Add(segment.Origin, Point.RotateRadian(frontScaled, radianToFront));
		Point<double> eyePointMirrored = Point.Add(segment.Origin, Point.RotateRadian(frontScaled, -radianToFront));

		Point<uint> dimensions = new(radius, radius);

		return [
			new EllipseData(dimensions, eyePoint, null, color),
			new EllipseData(dimensions, eyePointMirrored, null, color)
		];
	}
}
