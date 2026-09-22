using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.ShapeDataTypes;

/// <summary>
/// Describes a Bézier line.
/// </summary>
public class BezierLineData	: ShapeData
{
	protected Point<double>[] points;
	protected Point<double>? position;
	protected double? angle;

	/// <returns>The points of the line.</returns>
	public Point<double>[] Points => points;

	/// <returns>The origin position of the line.</returns>
	public Point<double>? Position => position;

	/// <returns>The angle of the line.</returns>
	public double? Angle => angle;

	/// <summary>
	/// Creates a <see cref="BezierLineData"/> object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="position">The origin position of the line.</param>
	/// <param name="angle">The angle of the line.</param>
	public BezierLineData(Point<double>[] points, Point<double>? position = null, double? angle = null, Color? color = null) : base(color)
	{
		this.points = points;
		this.position = position;
		this.angle = angle;
	}

	protected static void AddOneCurve(Point<double> prev, Point<double> current, Point<double> next, ref List<Point<double>> result)
	{
		Point<double> v = Point.Divide(Point.Subtract(prev, next), 6);

		result.Add(Point.Add(current, v));
		result.Add(current);
		result.Add(Point.Subtract(current, v));
	}

	/// <summary>
	/// Takes the points and creates a new array with midpoints.
	/// </summary>
	/// <param name="points">The original points.</param>
	/// <returns>The extended array.</returns>
	public static Point<double>[] MakeCubicBezierLine(Point<double>[] points)
	{
		int length = points.Length;

		if (points.Length < 3)
			return points;

		Point<double> prev = points[0];
		Point<double> current = points[1];
		Point<double> next;

		List<Point<double>> result = new(length * 3 + 1);

		{
			List<Point<double>> first = new(3);
			AddOneCurve(points[^1], prev, current, ref first);

			result.Add(first[1]);
			result.Add(first[2]);
		}

		for (int i = 2; i < points.Length; ++i)
		{
			next = points[i];

			AddOneCurve(prev, current, next, ref result);

			prev = current;
			current = next;
		}

		{
			List<Point<double>> last = new(3);
			AddOneCurve(prev, current, points[0], ref last);

			result.Add(last[0]);
			result.Add(last[1]);
		}

		return [.. result];
	}

	/// <summary>
	/// Takes the points and creates a new array with midpoints.
	/// </summary>
	/// <param name="points">The original points.</param>
	/// <returns>The extended array.</returns>
	public static Point<double>[] MakeCubicBezierLoop(Point<double>[] points)
	{
		int length = points.Length;

		if (points.Length < 3)
			return points;

		Point<double> prev = points[0];
		Point<double> current = points[1];
		Point<double> next;

		List<Point<double>> first = new(3);
		AddOneCurve(points[^1], prev, current, ref first);

		List<Point<double>> result = new(length * 3 + 1) { first[1], first[2] };

		for (int i = 2; i < points.Length; ++i)
		{
			next = points[i];

			AddOneCurve(prev, current, next, ref result);

			prev = current;
			current = next;
		}

		AddOneCurve(prev, current, points[0], ref result);

		result.Add(first[0]);
		result.Add(first[1]);

		return [.. result];
	}
}