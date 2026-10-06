using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes the back fin of a creature.
/// </summary>
internal class BackFin : BodyPart
{
	#region Fields

	protected int lengthInSegments;

	#endregion

	#region Properties

	/// <returns>The number of segments the fin will go through.</returns>
	public int LengthInSegments => lengthInSegments;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a <see cref="BackFin"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="lengthInSegments">The number of segments the fin will go through.</param>
	/// <param name="color">Color of the body part.</param>
	/// <exception cref="ArgumentException">A backfin must have a length of 2 or more.</exception>
	public BackFin(Segment segment, Render render, int lengthInSegments, Color color) : base(segment, render, color)
	{
		if (lengthInSegments < 2)
			throw new ArgumentException("A backfin must have a length of 2 or more!", nameof(lengthInSegments));

		this.lengthInSegments = lengthInSegments;
	}

	#endregion

	#region Public methods

	/// <summary>
	/// Calculates the outline points of the fin.
	/// </summary>
	/// <param name="fin">The fin to calculate with.</param>
	/// <returns>The calculated points.</returns>
	public static Point<double>[] GetPoints(BackFin fin)
	{
		List<Point<double>> points = [];

		int counter = 0;
		foreach (Segment nextSegment in fin.segment)
		{
			if (counter > fin.lengthInSegments)
				break;

			points.Add(nextSegment.Origin);
			++counter;
		}

		double angle = Point.SinOfPoints(points[^3], points[^2], points[^1]);

		for (int index = points.Count - 1; index > 0; --index)
		{
			Point<double> topPoint = Point.NormalRight(Point.Subtract(points[index - 1], points[index]));
			points.Add(Point.Add(points[index], Point.Multiply(topPoint, angle)));
		}

		return [.. points];
	}

	/// <summary>
	/// Draws this fin instance.
	/// </summary>
	public override List<ShapeData> Draw() => [new BezierLineData(GetPoints(this), null, null, color)];

	#endregion
}