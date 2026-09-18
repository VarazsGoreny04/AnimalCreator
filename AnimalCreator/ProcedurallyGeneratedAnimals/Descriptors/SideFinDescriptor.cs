using ProcedurallyGeneratedAnimals.BodyParts;
using System;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a fin.
/// </summary>
public class SideFinDescriptor : BodyPartDescriptor
{
	protected int length;
	protected int width;
	protected double angle;

	/// <returns>The length of the fin.</returns>
	public int Length => length;

	/// <returns>The width of the fin.</returns>
	public int Width => width;

	/// <returns>The angle of the fin.</returns>
	public double Angle => angle;

	/// <summary>
	/// Creates a SideFinDescriptor object.
	/// </summary>
	/// <param name="length">The length of the fin.</param>
	/// <param name="width">The width of the fin.</param>
	/// <param name="angle">The angle of the fin.</param>
	/// <param name="color">The color of the fin.</param>
	/// <param name="render">Where to render.</param>
	public SideFinDescriptor(int length, int width, double angle, Color color, Render render = Render.Bottom) : base(render, color)
	{
		this.length = Math.Abs(length);
		this.width = Math.Abs(width);
		this.angle = angle;
	}

	/// <summary>
	/// Creates a <see cref="SideFin"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="SideFin"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new SideFin(segment, render, length, width, angle, color);
}