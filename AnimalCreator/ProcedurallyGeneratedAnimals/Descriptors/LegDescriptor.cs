using ProcedurallyGeneratedAnimals.BodyParts;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a leg.
/// </summary>
public class LegDescriptor : BodyPartDescriptor
{
	protected LegSegmentDescriptor[] segmentDescriptors;
	protected Point<int> stepTo;

	/// <returns>The segments of the leg.</returns>
	public LegSegmentDescriptor[] SegmentDescriptors => segmentDescriptors;

	/// <returns>The position to step to.</returns>
	public Point<int> StepTo => stepTo;

	/// <summary>
	/// Creates a LegDescriptor object.
	/// </summary>
	/// <param name="legSegmentDescriptors">The segments of the leg.</param>
	/// <param name="stepTo">The position to step to.</param>
	/// <param name="color">The color of the leg.</param>
	/// <param name="render">Where to render.</param>
	public LegDescriptor(LegSegmentDescriptor[] legSegmentDescriptors, Point<int> stepTo, Color color, Render render = Render.Bottom) : base(render, color)
	{
		segmentDescriptors = legSegmentDescriptors;
		this.stepTo = stepTo;
	}

	/// <summary>
	/// Creates a <see cref="Leg"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="Leg"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new Leg(segment, render, segmentDescriptors, stepTo, color);
}
