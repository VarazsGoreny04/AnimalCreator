using ProcedurallyGeneratedAnimals.BodyParts;
using System;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a segment with turning angles.
/// </summary>
public class AngledSegmentDescriptor : SegmentDescriptor
{
	protected double minAngle;
	protected double maxAngle;

	/// <returns>The minimum angle of the joint.</returns>
	public double MinAngle => minAngle;

	/// <returns>The maximum angle of the joint.</returns>
	public double MaxAngle => maxAngle;

	/// <summary>
	/// Creates a LegSegmentDescriptor object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="minAngle">The minimum angle of the joint.</param>
	/// <param name="maxAngle">The maximum angle of the joint.</param>
	/// <param name="bodyPartDescriptors">The bodyParts of the segment.</param>
	public AngledSegmentDescriptor(int segmentDistance, int skinRadius, double minAngle, double maxAngle, BodyPartDescriptor[] bodyPartDescriptors)
		: base(segmentDistance, skinRadius, bodyPartDescriptors)
	{
		if (minAngle > maxAngle)
			throw new ArgumentException($"The maxAngle ({maxAngle}) should be bigger or equal than the minAngle ({minAngle})!");

		this.minAngle = minAngle;
		this.maxAngle = maxAngle;
	}

	/// <summary>
	/// Creates a LegSegmentDescriptor object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="angle">The minimum and maximum angle of the joint.</param>
	/// <param name="bodyPartDescriptors">The bodyParts of the segment.</param>
	public AngledSegmentDescriptor(int segmentDistance, int skinRadius, double angle, BodyPartDescriptor[] bodyPartDescriptors)
		: this(segmentDistance, skinRadius, -angle, angle, bodyPartDescriptors) { }

	/// <summary>
	/// Creates a LegSegmentDescriptor object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="minAngle">The minimum angle of the joint.</param>
	/// <param name="maxAngle">The maximum angle of the joint.</param>
	public AngledSegmentDescriptor(int segmentDistance, int skinRadius, double minAngle, double maxAngle)
		: this(segmentDistance, skinRadius, minAngle, maxAngle, []) { }

	/// <summary>
	/// Creates a LegSegmentDescriptor object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="angle">The minimum and maximum angle of the joint.</param>
	public AngledSegmentDescriptor(int segmentDistance, int skinRadius, double angle)
		: this(segmentDistance, skinRadius, -angle, angle, []) { }

	/// <summary>
	/// Creates a <see cref="Segment"/> object by this descriptor.
	/// </summary>
	/// <param name="prevOrigin">The origin of the previous segment.</param>
	/// <returns>The <see cref="Segment"/> object.</returns>
	internal override Segment Create(Point<double> prevOrigin)
	{
		return new(
			new Point<double>(prevOrigin.X, prevOrigin.Y - segmentDistance),
			Math.Abs(segmentDistance),
			skinRadius,
			bodyPartDescriptors,
			maxAngle,
			minAngle
		);
	}
}