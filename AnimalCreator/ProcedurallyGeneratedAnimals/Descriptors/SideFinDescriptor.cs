using ProcedurallyGeneratedAnimals.BodyParts;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a fin.
/// </summary>
public class SideFinDescriptor : BodyPartDescriptor
{
	protected Point<uint> dimensions;
	protected double angle;

	/// <returns>The width of the fin.</returns>
	public uint Width => dimensions.X;

	/// <returns>The length of the fin.</returns>
	public uint Length => dimensions.Y;

	/// <returns>The dimensions of the fin.</returns>
	public Point<uint> Dimensions => dimensions;

	/// <returns>The angle of the fin.</returns>
	public double Angle => angle;

	/// <summary>
	/// Creates a <see cref="SideFinDescriptor"/> object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the fin.</param>
	/// <param name="angle">The angle of the fin.</param>
	/// <param name="color">The color of the fin.</param>
	/// <param name="render">Where to render.</param>
	public SideFinDescriptor(Point<uint> dimensions, double angle, Color color, Render render = Render.Bottom) : base(render, color)
	{
		this.dimensions = dimensions;
		this.angle = angle;
	}

	/// <summary>
	/// Creates a <see cref="SideFinDescriptor"/> object.
	/// </summary>
	/// <param name="length">The length of the fin.</param>
	/// <param name="width">The width of the fin.</param>
	/// <param name="angle">The angle of the fin.</param>
	/// <param name="color">The color of the fin.</param>
	/// <param name="render">Where to render.</param>
	public SideFinDescriptor(uint width, uint length, double angle, Color color, Render render = Render.Bottom)
		: this(new Point<uint>(width, length), angle, color, render) { }

	/// <summary>
	/// Creates a <see cref="SideFin"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="SideFin"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new SideFin(segment, render, dimensions, angle, color);
}