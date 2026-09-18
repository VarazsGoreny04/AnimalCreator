namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a segment of an antenna.
/// </summary>
public class AntennaSegmentDescriptor : SegmentDescriptor
{
	protected double angle;

	/// <returns>The angle of the segment from the previous one.</returns>
	public double Angle => angle;

	/// <summary>
	/// Creates an AntennaSegmentDescriptor object.
	/// </summary>
	/// <param name="segmentDistance">The distance form the previous segment.</param>
	/// <param name="skinRadius">The radius of the skin at the segment.</param>
	/// <param name="angle">The angle of the segment from the previous one.</param>
	public AntennaSegmentDescriptor(int segmentDistance, int skinRadius, double angle) : base(segmentDistance, skinRadius) => this.angle = angle;

	/// <summary>
	/// Creates a <see cref="Segment"/> object by this descriptor.
	/// </summary>
	/// <param name="prevOrigin">The origin of the previous segment.</param>
	/// <returns>The <see cref="Segment"/> object.</returns>
	internal override Segment Create(Point<double> prevOrigin)
	{
		return new Segment(
			Point.RotateDegree(new Point<double>(prevOrigin.X - segmentDistance, prevOrigin.Y), angle),
			segmentDistance,
			skinRadius,
			[]
		);
	}
}