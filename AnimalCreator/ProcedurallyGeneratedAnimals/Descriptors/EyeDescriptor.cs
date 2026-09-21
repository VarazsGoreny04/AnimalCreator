using ProcedurallyGeneratedAnimals.BodyParts;
using System;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes an eye.
/// </summary>
public class EyeDescriptor : BodyPartDescriptor
{
	protected double degreeToFront;
	protected int distanceToOrigin;
	protected int radius;

	/// <returns>The angle to push the eye from the center of the segment.</returns>
	public double DegreeToFront => degreeToFront;

	/// <returns>The distance to push the eye from the center of the segment.</returns>
	public int DistanceToOrigin => distanceToOrigin;

	/// <returns>The radius of the eye.</returns>
	public int Radius => radius;

	/// <summary>
	/// Creates an <see cref="EyeDescriptor"/> object.
	/// </summary>
	/// <param name="angleToFront">The angle to push the eye from the center of the segment.</param>
	/// <param name="distanceToOrigin">The distance to push the eye from the center of the segment.</param>
	/// <param name="radius">The radius of the eye.</param>
	/// <param name="color">The color of the bodyPart.</param>
	/// <param name="render">Where to render.</param>
	public EyeDescriptor(double angleToFront, int distanceToOrigin, int radius, Color color, Render render = Render.Top) : base(render, color)
	{
		degreeToFront = Math.Abs(angleToFront);
		this.distanceToOrigin = Math.Abs(distanceToOrigin);
		this.radius = Math.Abs(radius);
	}

	/// <summary>
	/// Creates an <see cref="Eye"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="Eye"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new Eye(segment, render, degreeToFront, distanceToOrigin, radius, color);
}