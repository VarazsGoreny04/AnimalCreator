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
	private int speed;

	/// <returns>The head starting position.</returns>
	internal Segment HeadSegment => headSegment;

	/// <returns>The color of the animals body.</returns>
	public Color BodyColor => bodyColor;

	/// <returns>The speed of the animal.</returns>
	public int Speed => speed;

	public static event EventHandler<EllipseEventArgs>? DrawEllipse;
	public static event EventHandler<BezierLineEventArgs>? DrawBezierLine;

	/// <summary>
	/// Creates an <see cref="Animal"/> object.
	/// </summary>
	/// <param name="headPosition">The head starting position.</param>
	/// <param name="descriptors">The descriptors of the body of the animal.</param>
	/// <param name="bodyColor">The color of the animals body.</param>
	/// <param name="speed">The speed of the animal.</param>
	public Animal(Point<int> headPosition, SegmentDescriptor[] descriptors, Color bodyColor, int speed)
	{
		if (descriptors.Length < 2)
			throw new ArgumentException("An animal must have at least 2 segments!", nameof(descriptors));

		headSegment = Segment.CreateAndLink(headPosition, descriptors);

		this.bodyColor = bodyColor;
		this.speed = speed;
	}

	/// <summary>
	/// Invokes the DrawEllipse event.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="transforms">The transformations of the ellipse.</param>
	internal static void OnDrawEllipse(Point<int> dimensions, Transform[] transforms)
	{
		DrawEllipse?.Invoke(null, new EllipseEventArgs(dimensions, transforms));
	}

	/// <summary>
	/// Invokes the DrawEllipse event.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="transforms">The transformations of the ellipse.</param>
	/// <param name="color">The color of the ellipse.</param>
	internal static void OnDrawEllipse(Point<int> dimensions, Transform[] transforms, Color color)
	{
		DrawEllipse?.Invoke(null, new EllipseEventArgs(dimensions, transforms, color));
	}

	/// <summary>
	/// Invokes the DrawBezierLine event.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="transformations">The transformations of the line.</param>
	internal static void OnDrawBezierLine(Point<double>[] points, Transform[] transformations)
	{
		DrawBezierLine?.Invoke(null, new BezierLineEventArgs(points, transformations));
	}

	/// <summary>
	/// Invokes the DrawBezierLine event.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="transformations">The transformations of the line.</param>
	/// <param name="color">The color of the line.</param>
	internal static void OnDrawBezierLine(Point<double>[] points, Transform[] transformations, Color color)
	{
		DrawBezierLine?.Invoke(null, new BezierLineEventArgs(points, transformations, color));
	}

	/// <summary>
	/// Draws a line on the spine of the animal.
	/// </summary>
	/// <param name="animal">The animal.</param>
	public static void DrawSpine(Animal animal)
	{
		List<Point<double>> points = [];
		foreach (Segment segment in animal.headSegment)
			points.Add(segment.Origin);

		OnDrawBezierLine([.. points], []);
	}

	/// <summary>
	/// Draws a circle to every segment of the body.
	/// </summary>
	/// <param name="animal">The animal.</param>
	public static void DrawCircles(Animal animal)
	{
		foreach (Segment segment in animal.headSegment)
			OnDrawEllipse(new Point<int>(segment.SkinRadius, segment.SkinRadius), [new Translate(segment.Origin)]);
	}

	/// <summary>
	/// Draws the outline of the animal.
	/// </summary>
	/// <param name="animal">The animal.</param>
	public static void DrawOutline(Animal animal) => OnDrawBezierLine(Segment.GetPoints(animal.headSegment), [], animal.bodyColor);

	/// <summary>
	/// Draws this animal instance.
	/// </summary>
	public void Draw()
	{
		foreach (Segment segment in headSegment)
			Segment.DrawBodyParts(segment, Render.Bottom);

		DrawOutline(this);
		// DrawCircles(this);
		// DrawSpine(this);

		foreach (Segment segment in headSegment)
			Segment.DrawBodyParts(segment, Render.Top);
	}

	/// <summary>
	/// Moves this animal instance to the given direction.
	/// </summary>
	/// <param name="destination">The given direction.</param>
	public void Step(Point<int> destination)
	{
		Point<double> vectorToDestination = Point.Subtract(Point.IntToDouble(destination), headSegment.Origin);

		if (Point.Magnitude(vectorToDestination) < speed)
			return;

		Point<double> direction = Point.Scale(vectorToDestination, speed);

		Point<double> restrictedDirection = headSegment.NextSegment is not null ?
			Segment.RestrictAngleOfRotation(headSegment, headSegment.NextSegment, direction) : direction;

		headSegment.Origin = Point.Add(headSegment.Origin, restrictedDirection);
		Segment.PullNext(headSegment);
	}
}