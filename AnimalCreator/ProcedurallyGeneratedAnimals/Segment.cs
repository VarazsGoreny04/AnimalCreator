using System;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals;

/// <summary>
/// Describes a segment of a creature.
/// </summary>
internal class Segment
{
	protected Point<double> origin;
	protected int distanceFromPrev;
	protected int skinRadius;
	protected double maxAngle;
	protected double minAngle;
	protected BodyPart[] bodyParts;
	protected Segment? prevSegment;
	protected Segment? nextSegment;

	/// <summary>
	/// Gets and sets the position of the segment.
	/// </summary>
	public Point<double> Origin { get => origin; set => origin = value; }

	/// <returns>The distance of this segment from the previous one.</returns>
	public int DistanceFromPrev => distanceFromPrev;

	/// <returns>The width of the creature at this segment.</returns>
	public int SkinRadius => skinRadius;

	/// <returns>The minimum angle of rotation at this segment.</returns>
	public double MaxAngle => maxAngle;

	/// <returns>The minimum angle of rotation at this segment.</returns>
	public double MinAngle => minAngle;

	/// <returns>The additional bodyParts.</returns>
	public BodyPart[] BodyParts => bodyParts;

	/// <returns>The previous segment.</returns>
	public Segment? PrevSegment => prevSegment;

	/// <returns>The next segment.</returns>
	public Segment? NextSegment => nextSegment;

	/// <summary>
	/// Creates a Segment object.
	/// </summary>
	/// <param name="origin">The position of the segment.</param>
	/// <param name="distanceFromPrev">The distance of this segment from the previous one.</param>
	/// <param name="skinRadius">The width of the creature at this segment.</param>
	/// <param name="bodyParts">The additional bodyParts.</param>
	/// <param name="minAngle">The minimum angle of rotation at this segment.</param>
	/// <param name="maxAngle">The maximum angle of rotation at this segment.</param>
	public Segment(Point<double> origin, int distanceFromPrev, int skinRadius, BodyPart[] bodyParts, double minAngle, double maxAngle)
	{
		this.origin = origin;
		this.distanceFromPrev = distanceFromPrev;
		this.skinRadius = skinRadius;
		this.maxAngle = maxAngle;
		this.minAngle = minAngle;

		this.bodyParts = bodyParts;
		foreach (BodyPart bodyPart in bodyParts)
			bodyPart.Segment = this;

		prevSegment = null;
		nextSegment = null;
	}

	/// <summary>
	/// Creates a Segment object.
	/// </summary>
	/// <param name="origin">The position of the segment.</param>
	/// <param name="distanceFromPrev">The distance of this segment from the previous one.</param>
	/// <param name="skinRadius">The width of the creature at this segment.</param>
	/// <param name="bodyParts">The additional bodyParts.</param>
	public Segment(Point<double> origin, int distanceFromPrev, int skinRadius, BodyPart[] bodyParts) : this(origin, distanceFromPrev, skinRadius, bodyParts, 0, 0)
	{
		maxAngle = Math.Min(20 * this.distanceFromPrev / this.skinRadius, 60);
		minAngle = -maxAngle;
	}

	/// <summary>
	/// Creates the segments by the given descriptors and links them together.
	/// </summary>
	/// <param name="startingPoint">The starting position of the first segment.</param>
	/// <param name="segmentDescriptors">The descriptors of the segments.</param>
	/// <returns>The head segment of the linked list.</returns>
	internal static Segment CreateAndLink(Point<double> startingPoint, SegmentDescriptor[] segmentDescriptors)
	{
		Segment result = segmentDescriptors[0].Create(startingPoint);

		Segment prev = result;
		Segment next;

		int descriptorsLength = segmentDescriptors.Length;
		for (int i = 1; i < descriptorsLength; ++i)
		{
			next = segmentDescriptors[i].Create(prev.Origin);

			prev.nextSegment = next;
			next.prevSegment = prev;

			prev = next;
		}

		PullNext(result);

		return result;
	}

	/// <summary>
	/// Creates the segments by the given descriptors and links them together.
	/// </summary>
	/// <param name="startingPoint">The starting position of the first segment.</param>
	/// <param name="segmentDescriptors">The descriptors of the segments.</param>
	/// <returns>The head segment of the linked list.</returns>
	public static Segment CreateAndLink(Point<int> startingPoint, SegmentDescriptor[] segmentDescriptors)
	{
		return CreateAndLink(Point.IntToDouble(startingPoint), segmentDescriptors);
	}

	/// <summary>
	/// Iterates through the segments.
	/// </summary>
	/// <returns>The iterable sequence.</returns>
	public IEnumerator<Segment> GetEnumerator()
	{
		Segment? current = this;

		while (current is not null)
		{
			yield return current;
			current = current.nextSegment;
		}
	}

	/// <summary>
	/// Pulls the given neighbour segment towards this segment.
	/// </summary>
	/// <param name="segment">The segment to pull towards.</param>
	/// <param name="segmentToPull">The segment to pull.</param>
	/// <param name="distanceBetween">The needed distance in between the two segments.</param>
	/// <param name="segmentInFront">The segment on the other side of the main segment.</param>
	public static void Pull(Segment segment, Segment segmentToPull, int distanceBetween, Segment? segmentInFront = null)
	{
		Point<double> fromSegmentToNext = Point.Subtract(segmentToPull.origin, segment.origin);
		Point<double> toJoinPoint = Point.Scale(fromSegmentToNext, distanceBetween);

		if (segmentInFront is not null)
			toJoinPoint = RestrictAngleOfRotation(segment, segmentInFront, toJoinPoint);

		segmentToPull.origin = Point.Add(segment.origin, toJoinPoint);
	}

	/// <summary>
	/// Pulls the next segment of the given segment.
	/// </summary>
	/// <param name="segment">The given segment.</param>
	public static void PullNext(Segment segment)
	{
		if (segment.nextSegment is not null)
		{
			Pull(segment, segment.nextSegment, segment.nextSegment.distanceFromPrev, segment.prevSegment);
			PullNext(segment.nextSegment);
		}
	}

	/// <summary>
	/// Pulls the previous segment of the given segment.
	/// </summary>
	/// <param name="segment">The given segment.</param>
	public static void PullPrev(Segment segment)
	{
		if (segment.prevSegment is not null)
		{
			Pull(segment, segment.prevSegment, segment.distanceFromPrev, segment.nextSegment);
			PullPrev(segment.prevSegment);
		}
	}

	/// <summary>
	/// Gets the front vector of the given segment.
	/// </summary>
	/// <param name="segment">The given segment.</param>
	/// <exception cref="ArgumentException">If the segment has no neighbours.</exception>
	/// <returns>The calculated vector.</returns>
	public static Point<double> GetFrontVector(Segment segment)
	{
		Segment? prev = segment.prevSegment;
		Segment? next = segment.nextSegment;

		if (prev is null && next is null)
			throw new ArgumentException("Not enough segments!");

		prev ??= segment;
		next ??= segment;

		Point<double> vector = Point.Subtract(prev.origin, next.origin);

		return Point.Scale(vector, segment.skinRadius);
	}

	/// <summary>
	/// Gets the outline points of the segment list.
	/// </summary>
	/// <param name="headSegment">The head segment.</param>
	/// <returns>The outline points.</returns>
	public static Point<double>[] GetPoints(Segment headSegment)
	{
		double roundNoseAngle = double.DegreesToRadians(45);

		Point<double> frontVector = GetFrontVector(headSegment);

		List<Point<double>> left = [
			Point.Add(headSegment.origin, frontVector),
			Point.Add(headSegment.origin, Point.RotateRadian(frontVector, roundNoseAngle))
		];
		List<Point<double>> right = [
			Point.Add(headSegment.origin, Point.RotateRadian(frontVector, -roundNoseAngle))
		];

		Segment tailSegment = headSegment;

		foreach (Segment segment in headSegment)
		{
			Point<double> front = GetFrontVector(segment);

			left.Add(Point.Add(segment.origin, Point.NormalLeft(front)));
			right.Add(Point.Add(segment.origin, Point.NormalRight(front)));

			tailSegment = segment;
		}

		Point<double> backVector = Point.Reverse(GetFrontVector(tailSegment));

		left.Add(Point.Add(tailSegment.origin, Point.RotateRadian(backVector, -roundNoseAngle)));
		right.Add(Point.Add(tailSegment.origin, Point.RotateRadian(backVector, roundNoseAngle)));

		left.Add(Point.Add(tailSegment.origin, backVector));

		left.Reverse();
		left.AddRange(right);

		return [.. left];
	}

	/// <summary>
	/// Draws all the bodyParts of the given segment set to the given render mode.
	/// </summary>
	/// <param name="segment">The segment with the bodyParts.</param>
	/// <param name="render">The render mode.</param>
	public static void DrawBodyParts(Segment segment, Render render)
	{
		foreach (BodyPart bodyPart in segment.bodyParts)
		{
			if (bodyPart.Render == render)
				bodyPart.Draw();
		}
	}

	/// <summary>
	/// Restricts the given direction vector by the vector created between the two segments and the angle properties of the first segment.
	/// </summary>
	/// <param name="firstSegment">The first segment.</param>
	/// <param name="secondSegment">The second segment.</param>
	/// <param name="direction">The vector to restrict.</param>
	/// <returns>The restricted vector.</returns>
	public static Point<double> RestrictAngleOfRotation(Segment firstSegment, Segment secondSegment, Point<double> direction)
	{
		Point<double> fromSecondToFirst = Point.Subtract(firstSegment.origin, secondSegment.origin);

		return Point.RestrictAngleOfRotation(fromSecondToFirst, direction, firstSegment.maxAngle, firstSegment.minAngle);
	}
}