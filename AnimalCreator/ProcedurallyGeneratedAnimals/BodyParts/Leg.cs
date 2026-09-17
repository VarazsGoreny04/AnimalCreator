using ProcedurallyGeneratedAnimals.Descriptors;
using System;
using System.Collections.Generic;

namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes one pair of legs of a creature.
/// </summary>
internal class Leg : BodyPart
{
	/// <summary>
	/// Describes one leg of a creature.
	/// </summary>
	protected class OneLeg
	{
		protected Segment headSegment;
		protected Segment tailSegment;
		protected Point<double> standsOn;
		protected double range;

		/// <returns>The head segment of the leg.</returns>
		public Segment HeadSegment => headSegment;

		/// <returns>The tail segment of the leg.</returns>
		public Segment TailSegment => tailSegment;

		/// <summary>
		/// Gets or sets the point the leg stands on.
		/// </summary>
		public Point<double> StandsOn { get => standsOn; set => standsOn = value; }

		/// <returns>The maximum distance between the head segment and the tail segment.</returns>
		public double Range => range;

		/// <summary>
		/// Creates a <see cref="OneLeg"/> object.
		/// </summary>
		/// <param name="origin">The origin of the parent segment.</param>
		/// <param name="descriptors">The descriptors of the segments of the leg.</param>
		public OneLeg(Point<int> origin, SegmentDescriptor[] descriptors)
		{
			if (descriptors.Length < 2)
				throw new ArgumentException("A leg must have at least 2 segment descriptors!", nameof(descriptors));

			headSegment = Segment.CreateAndLink(origin, descriptors);

			Segment tail = headSegment;
			foreach (Segment nextSegment in headSegment)
				tail = nextSegment;

			tailSegment = tail;
			standsOn = tailSegment.Origin;

			range = Point.Distance(tailSegment.Origin, headSegment.Origin);
		}

		/// <summary>
		/// Gets a new step location for the given leg.
		/// </summary>
		/// <param name="leg">The leg to get a new target for.</param>
		/// <param name="frontVector">The normalized front vector of the parent segment.</param>
		/// <param name="normalVector">The normalized normal vector of the parent segment pointing towards the legs direction.</param>
		/// <param name="stepStyler">The direction vector to calculate the location of the next step.</param>
		/// <returns>The new location to step to.</returns>
		public static Point<double> GetNewTarget(OneLeg leg, Point<double> frontVector, Point<double> normalVector, Point<double> stepStyler)
		{
			Point<double> toSide = Point.Multiply(normalVector, stepStyler.X);
			Point<double> toFront = Point.Multiply(frontVector, stepStyler.Y);
			Point<double> direction = Point.Add(toSide, toFront);

			return Point.Add(leg.headSegment.Origin, Point.Magnitude(direction) > leg.range ? Point.Scale(direction, leg.range) : direction);
		}

		/// <summary>
		/// Performs two way inverse kinematics on the given leg.
		/// </summary>
		/// <param name="leg">The given leg.</param>
		public static void TwoWayKinematics(OneLeg leg)
		{
			Point<double> joinPoint = leg.headSegment.Origin;

			leg.tailSegment.Origin = leg.standsOn;
			Segment.PullPrev(leg.tailSegment);

			leg.headSegment.Origin = joinPoint;
			Segment.PullNext(leg.headSegment);
		}

		/// <summary>
		/// Mirrors the position of the points of the leg around a certain point.
		/// </summary>
		/// <param name="origin">The point to mirror around.</param>
		/// <param name="leg">The given leg.</param>
		public static void Break(Point<double> origin, OneLeg leg)
		{
			foreach (Segment segment in leg.headSegment)
				segment.Origin = Point.Subtract(Point.Multiply(origin, 2), segment.Origin);
		}

		/// <summary>
		/// Draws a leg instance.
		/// </summary>
		/// <param name="leg">The leg to draw.</param>
		/// <param name="color">The color of the leg.</param>
		public static void Draw(OneLeg leg, Color color)
		{
			TwoWayKinematics(leg);

			double distanceFromTarget = Point.Magnitude(Point.Subtract(leg.standsOn, leg.tailSegment.Origin));

			if (distanceFromTarget > leg.tailSegment.DistanceFromPrev)
			{
				Break(leg.headSegment.Origin, leg);

				for (int i = 0; i < 5; ++i)
					TwoWayKinematics(leg);
			}

			foreach (Segment segment in leg.headSegment)
				Segment.DrawBodyParts(segment, Render.Bottom);

			Animal.OnDrawBezierLine(Segment.GetPoints(leg.headSegment), [], color);

			foreach (Segment segment in leg.headSegment)
				Segment.DrawBodyParts(segment, Render.Top);
		}
	}

	protected OneLeg left;
	protected OneLeg right;
	protected Point<double> stepTo;

	/// <summary>
	/// Gets or sets the point the leg stands on.
	/// </summary>
	public Point<double> StepTo { get => stepTo; set => stepTo = value; }

	/// <summary>
	/// Creates a <see cref="Leg"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="descriptors">The descriptors of the segments of one leg.</param>
	/// <param name="stepTo">Point to step on.</param>
	/// <param name="color">The color of the legs.</param>
	public Leg(Segment segment, Render render, LegSegmentDescriptor[] descriptors, Point<int> stepTo, Color color) : base(segment, render, color)
	{
		Point<int> origin = Point.DoubleToInt(segment.Origin);

		List<SegmentDescriptor> mirroredDescriptors = new(descriptors.Length);
		foreach (LegSegmentDescriptor descriptor in descriptors)
			mirroredDescriptors.Add(LegSegmentDescriptor.Mirror(descriptor));

		left = new OneLeg(origin, descriptors);
		right = new OneLeg(origin, [.. mirroredDescriptors]);

		this.stepTo = Point.IntToDouble(stepTo);
	}

	/// <summary>
	/// Draws one leg.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="frontVector">The normalized front vector of the parent segment.</param>
	/// <param name="normalVector">The normalized normal vector of the parent segment pointing towards the legs direction.</param>
	/// <param name="leg">The leg to draw.</param>
	/// <param name="color">The color of the leg.</param>
	/// <param name="stepTo">Point to step on.</param>
	protected static void DrawOne(Segment segment, Point<double> frontVector, Point<double> normalVector, OneLeg leg, Color color, Point<double> stepTo)
	{
		leg.HeadSegment.Origin = Point.Add(segment.Origin, Point.Scale(normalVector, leg.HeadSegment.DistanceFromPrev));

		double distanceFromTarget = Point.Distance(leg.StandsOn, leg.HeadSegment.Origin);
		double bodyLegAngle = Math.Abs(Point.AngleOfVectors(frontVector, Point.Subtract(leg.HeadSegment.Origin, leg.HeadSegment.NextSegment!.Origin)));

		if (distanceFromTarget > leg.Range || bodyLegAngle < 30)
			leg.StandsOn = OneLeg.GetNewTarget(leg, frontVector, normalVector, stepTo);

		OneLeg.Draw(leg, color);
	}

	/// <summary>
	/// Draws this leg instance.
	/// </summary>
	public override void Draw()
	{
		Point<double> normalizedFrontVector = Point.Normalize(Segment.GetFrontVector(segment));

		DrawOne(segment, normalizedFrontVector, Point.NormalRight(normalizedFrontVector), left, color, stepTo);
		DrawOne(segment, normalizedFrontVector, Point.NormalLeft(normalizedFrontVector), right, color, stepTo);
	}
}