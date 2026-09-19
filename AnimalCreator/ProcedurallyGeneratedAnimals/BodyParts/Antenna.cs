using ProcedurallyGeneratedAnimals.Descriptors;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes a pair of antennas of a creature.
/// </summary>
internal class Antenna : BodyPart
{
	protected Point<double>[] points;
	protected Point<double>[] pointsMirrored;
	protected double angle;

	/// <returns>The points of the antenna.</returns>
	public Point<double>[] Points => points;

	/// <returns>The points of the antenna mirrored.</returns>
	public Point<double>[] PointsMirrored => pointsMirrored;

	/// <returns>The angle between the antenna and the spine of the animal.</returns>
	public double Angle => angle;

	/// <summary>
	/// Creates a <see cref="TailFin"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="descriptors">The descriptors of the segments of the antenna.</param>
	/// <param name="angle">The angle of the antennas.</param>
	/// <param name="color">Color of the body part.</param>
	public Antenna(Segment segment, Render render, SegmentDescriptor[] descriptors, double angle, Color color) : base(segment, render, color)
	{
		points = Segment.GetPoints(Segment.CreateAndLink(new Point<int>(0, 0), descriptors));

		if (Math.Abs(this.angle) < 1)
			pointsMirrored = [];
		else
		{
			pointsMirrored = new Point<double>[points.Length];

			for (int i = points.Length - 1; i >= 0; --i)
				pointsMirrored[i] = new Point<double>(points[i].X, -points[i].Y);
		}

		this.angle = angle;
	}

	/// <summary>
	/// Draws this antenna instance.
	/// </summary>
	public override List<ShapeData> Draw()
	{
		double bodyAngle = Point.AngleOfVector(Point.Reverse(Segment.GetFrontVector(segment)));

		return [
			new BezierLineData(points, segment.Origin, bodyAngle + angle, color),
			new BezierLineData(pointsMirrored, segment.Origin, bodyAngle - angle, color)
		];
	}
}