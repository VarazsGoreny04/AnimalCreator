using System;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a segment.
/// </summary>
public class SegmentDescriptor
{
	#region Fields

	protected int segmentDistance;
	protected uint skinRadius;
	protected BodyPartDescriptor[] bodyPartDescriptors;

	#endregion

	#region Properties

	/// <returns>The distance form the previous segment.</returns>
	public int SegmentDistance => segmentDistance;

	/// <returns>The radius of the skin at the segment.</returns>
	public uint SkinRadius => skinRadius;

	/// <returns>The bodyParts of the segment.</returns>
	public BodyPartDescriptor[] BodyPartDescriptors => bodyPartDescriptors;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a <see cref="SegmentDescriptor"/> object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="bodyPartDescriptors">The bodyParts of the segment.</param>
	public SegmentDescriptor(int segmentDistance, uint skinRadius, BodyPartDescriptor[] bodyPartDescriptors)
	{
		this.segmentDistance = segmentDistance;
		this.skinRadius = skinRadius;
		this.bodyPartDescriptors = bodyPartDescriptors;
	}

	/// <summary>
	/// Creates a <see cref="SegmentDescriptor"/> object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	public SegmentDescriptor(int segmentDistance, uint skinRadius) : this(segmentDistance, skinRadius, []) { }

	#endregion

	#region Internal methods

	/// <summary>
	/// Creates a Segment object by this descriptor.
	/// </summary>
	/// <param name="prevOrigin">The origin of the previous segment.</param>
	/// <returns>The Segment object.</returns>
	internal virtual Segment Create(Point<double> prevOrigin)
	{
		return new(
			new Point<double>(prevOrigin.X - segmentDistance, prevOrigin.Y),
			(uint)Math.Abs(segmentDistance),
			skinRadius,
			bodyPartDescriptors
		);
	}

	#endregion
}