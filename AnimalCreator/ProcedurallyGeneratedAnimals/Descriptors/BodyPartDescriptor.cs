using ProcedurallyGeneratedAnimals.BodyParts;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a body part.
/// </summary>
public abstract class BodyPartDescriptor
{
	protected Render render;
	protected Color color;

	/// <returns>Where to render.</returns>
	public Render Render => render;

	/// <returns>The color of the bodyPart.</returns>
	public Color Color => color;

	/// <summary>
	/// Creates a BodyPartDescriptor object.
	/// </summary>
	/// <param name="render">Where to render.</param>
	/// <param name="color">The color of the bodyPart.</param>
	public BodyPartDescriptor(Render render, Color color)
	{
		this.render = render;
		this.color = color;
	}

	/// <summary>
	/// Creates a <see cref="BodyParts"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="BodyParts"/> object.</returns>
	internal abstract BodyPart Create(Segment segment);
}