using ProcedurallyGeneratedAnimals.BodyParts;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes an eye.
/// </summary>
public class EyeDescriptor : BodyPartDescriptor
{
	protected double angleToFront;
	protected uint distanceToOrigin;
	protected uint radius;

	/// <returns>The angle to push the eye from the center of the segment.</returns>
	public double AngleToFront => angleToFront;

	/// <returns>The distance to push the eye from the center of the segment.</returns>
	public uint DistanceToOrigin => distanceToOrigin;

	/// <returns>The radius of the eye.</returns>
	public uint Radius => radius;

	/// <summary>
	/// Creates an <see cref="EyeDescriptor"/> object.
	/// </summary>
	/// <param name="angleToFront">The angle to push the eye from the center of the segment.</param>
	/// <param name="distanceToOrigin">The distance to push the eye from the center of the segment.</param>
	/// <param name="radius">The radius of the eye.</param>
	/// <param name="color">The color of the bodyPart.</param>
	/// <param name="render">Where to render.</param>
	public EyeDescriptor(double angleToFront, uint distanceToOrigin, uint radius, Color color, Render render = Render.Top) : base(render, color)
	{
		this.angleToFront = angleToFront;
		this.distanceToOrigin = distanceToOrigin;
		this.radius = radius;
	}

	/// <summary>
	/// Creates an <see cref="Eye"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="Eye"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new Eye(segment, render, angleToFront, distanceToOrigin, radius, color);
}