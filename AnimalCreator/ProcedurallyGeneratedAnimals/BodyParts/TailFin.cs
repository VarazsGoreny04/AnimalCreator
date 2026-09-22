using ProcedurallyGeneratedAnimals.Descriptors;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes the tail fin of a creature.
/// </summary>
internal class TailFin : BodyPart
{
	protected Segment headSegment;

	/// <returns>The head segment of the fin.</returns>
	public Segment HeadSegment => headSegment;

	/// <summary>
	/// Creates a <see cref="TailFin"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="distances">The distances between the segments of the fin.</param>
	/// <param name="color">Color of the body part.</param>
	public TailFin(Segment segment, Render render, int[] distances, Color color) : base(segment, render, color)
	{
		if (distances.Length < 2)
			throw new ArgumentException("A tailfin must have at least 2 distance descriptors!", nameof(distances));

		List<SegmentDescriptor> descriptors = [new SegmentDescriptor(1, 1)];
		foreach (int distance in distances)
			descriptors.Add(new SegmentDescriptor(distance, 1));

		headSegment = Segment.CreateAndLink(segment.Origin, [.. descriptors]);
	}

	/// <summary>
	/// Calculates the outline points of the fin.
	/// </summary>
	/// <param name="fin">The fin to calculate with.</param>
	/// <returns>The calculated points.</returns>
	public static Point<double>[] GetPoints(TailFin fin)
	{
		List<Point<double>> points = [];

		foreach (Segment nextSegment in fin.headSegment)
			points.Add(nextSegment.Origin);

		double angle = Point.SinOfPoints(points[^3], points[^2], points[^1]);
		double magicMultiplier = 13 * angle / (points.Count - 1);

		for (int index = points.Count - 1; index > 0; --index)
		{
			Point<double> topPoint = Point.NormalRight(Point.Subtract(points[index - 1], points[index]));
			points.Add(Point.Add(points[index], Point.Multiply(topPoint, index * magicMultiplier)));
		}

		return [.. points];
	}

	/// <summary>
	/// Draws this fin instance.
	/// </summary>
	public override List<ShapeData> Draw()
	{
		headSegment.Origin = segment.Origin;
		Segment.PullNext(headSegment);

		return [new BezierLineData(GetPoints(this), null, null, color)];
	}
}
