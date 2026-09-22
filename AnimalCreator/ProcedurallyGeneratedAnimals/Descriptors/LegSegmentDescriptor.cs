using System;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a segment of a leg.
/// </summary>
public class LegSegmentDescriptor : AngledSegmentDescriptor
{
	/// <summary>
	/// Creates a <see cref="LegSegmentDescriptor"/> object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="minAngle">The minimum angle of the joint.</param>
	/// <param name="maxAngle">The maximum angle of the joint.</param>
	/// <param name="bodyPartDescriptors">The bodyParts of the segment.</param>
	public LegSegmentDescriptor(int segmentDistance, uint skinRadius, double minAngle, double maxAngle, BodyPartDescriptor[] bodyPartDescriptors)
		: base(segmentDistance, skinRadius, minAngle, maxAngle, bodyPartDescriptors) { }

	/// <summary>
	/// Creates a <see cref="LegSegmentDescriptor"/> object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="minAngle">The minimum angle of the joint.</param>
	/// <param name="maxAngle">The maximum angle of the joint.</param>
	public LegSegmentDescriptor(int segmentDistance, uint skinRadius, double minAngle, double maxAngle)
		: this(segmentDistance, skinRadius, minAngle, maxAngle, []) { }

	/// <summary>
	/// Mirrors the position one <see cref="LegSegmentDescriptor"/>.
	/// </summary>
	/// <param name="descriptor">The <see cref="LegSegmentDescriptor"/> to mirror.</param>
	/// <returns>A new <see cref="LegSegmentDescriptor"/> object with mirrored coordinates.</returns>
	internal static LegSegmentDescriptor Mirror(LegSegmentDescriptor descriptor)
	{
		return new LegSegmentDescriptor(
			-descriptor.segmentDistance,
			descriptor.skinRadius,
			-descriptor.maxAngle,
			-descriptor.minAngle,
			descriptor.bodyPartDescriptors
		);
	}

	/// <summary>
	/// Creates a <see cref="Segment"/> object by this descriptor.
	/// </summary>
	/// <param name="prevOrigin">The origin of the previous segment.</param>
	/// <returns>The <see cref="Segment"/> object.</returns>
	internal override Segment Create(Point<double> prevOrigin)
	{
		return new Segment(
			new Point<double>(prevOrigin.X, prevOrigin.Y - segmentDistance),
			(uint)Math.Abs(segmentDistance),
			skinRadius,
			bodyPartDescriptors,
			minAngle,
			maxAngle
		);
	}
}