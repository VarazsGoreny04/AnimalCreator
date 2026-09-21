using ProcedurallyGeneratedAnimals.Descriptors;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals;

/// <summary>
/// Describes an animal.
/// </summary>
public sealed class Animal
{
	private Segment headSegment;
	private Color bodyColor;
	private uint speed;

	/// <returns>The head position.</returns>
	public Point<double> HeadPosition => headSegment.Origin;

	/// <returns>The color of the animals body.</returns>
	public Color BodyColor => bodyColor;

	/// <returns>The speed of the animal.</returns>
	public uint Speed => speed;

	/// <summary>
	/// Creates an <see cref="Animal"/> object.
	/// </summary>
	/// <param name="headPosition">The head starting position.</param>
	/// <param name="descriptors">The descriptors of the body of the animal.</param>
	/// <param name="bodyColor">The color of the animals body.</param>
	/// <param name="speed">The speed of the animal.</param>
	public Animal(Point<int> headPosition, SegmentDescriptor[] descriptors, Color bodyColor, uint speed)
	{
		if (descriptors.Length < 2)
			throw new ArgumentException("An animal must have at least 2 segments!", nameof(descriptors));

		headSegment = Segment.CreateAndLink(headPosition, descriptors);

		this.bodyColor = bodyColor;
		this.speed = speed;
	}

	/// <summary>
	/// Draws a line on the spine of the animal.
	/// </summary>
	/// <param name="animal">The animal.</param>
	public static BezierLineData DrawSpine(Animal animal)
	{
		List<Point<double>> points = [];
		foreach (Segment segment in animal.headSegment)
			points.Add(segment.Origin);

		return new BezierLineData([.. points]);
	}

	/// <summary>
	/// Draws a circle to every segment of the body.
	/// </summary>
	/// <param name="animal">The animal.</param>
	public static List<EllipseData> DrawCircles(Animal animal)
	{
		List<EllipseData> circles = [];

		foreach (Segment segment in animal.headSegment)
			circles.Add(new EllipseData(new Point<uint>(segment.SkinRadius, segment.SkinRadius), segment.Origin));

		return circles;
	}

	/// <summary>
	/// Draws the outline of the animal.
	/// </summary>
	/// <param name="animal">The animal.</param>
	public static BezierLineData DrawOutline(Animal animal) => new(Segment.GetPoints(animal.headSegment), null, null, animal.bodyColor);

	/// <summary>
	/// Draws this animal instance.
	/// </summary>
	public ShapeData[] Draw()
	{
		List<ShapeData> shapes = [];

		foreach (Segment segment in headSegment)
			shapes.AddRange(Segment.DrawBodyParts(segment, Render.Bottom));

		shapes.Add(DrawOutline(this));
		// shapes.AddRange(DrawCircles(this));
		// shapes.Add(DrawSpine(this));

		foreach (Segment segment in headSegment)
			shapes.AddRange(Segment.DrawBodyParts(segment, Render.Top));

		return [.. shapes];
	}

	/// <summary>
	/// Moves this animal instance to the given direction.
	/// </summary>
	/// <param name="destination">The given direction.</param>
	public void Step(Point<double> destination)
	{
		Point<double> vectorToDestination = Point.Subtract(destination, headSegment.Origin);

		if (Point.Magnitude(vectorToDestination) < speed)
			return;

		Point<double> direction = Point.Scale(vectorToDestination, speed);

		Point<double> restrictedDirection = headSegment.NextSegment is not null ?
			Segment.RestrictAngleOfRotation(headSegment, headSegment.NextSegment, direction) : direction;

		headSegment.Origin = Point.Add(headSegment.Origin, restrictedDirection);
		Segment.PullNext(headSegment);
	}
}