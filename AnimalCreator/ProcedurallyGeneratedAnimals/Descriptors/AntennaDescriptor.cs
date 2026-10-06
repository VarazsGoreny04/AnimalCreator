using ProcedurallyGeneratedAnimals.BodyParts;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes an antenna.
/// </summary>
public class AntennaDescriptor : BodyPartDescriptor
{
	#region Fields

	protected AntennaSegmentDescriptor[] segmentDescriptors;
	protected double angle;

	#endregion

	#region Properties

	/// <returns>The segments of the antenna.</returns>
	public AntennaSegmentDescriptor[] SegmentDescriptors => segmentDescriptors;

	/// <returns>The angle to push the eye from the center of the segment.</returns>
	public double Angle => angle;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates an <see cref="AntennaDescriptor"/> object.
	/// </summary>
	/// <param name="antennaSegmentDescriptors">The segments of the antenna.</param>
	/// <param name="angle">The angle to push the eye from the center of the segment.</param>
	/// <param name="color">The color of the antenna.</param>
	/// <param name="render">Where to render.</param>
	public AntennaDescriptor(AntennaSegmentDescriptor[] antennaSegmentDescriptors, double angle, Color color, Render render = Render.Top) : base(render, color)
	{
		segmentDescriptors = antennaSegmentDescriptors;
		this.angle = angle;
	}

	#endregion

	#region Internal methods

	/// <summary>
	/// Creates an <see cref="Antenna"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="Antenna"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new Antenna(segment, render, segmentDescriptors, angle, color);

	#endregion
}