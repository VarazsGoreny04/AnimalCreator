namespace ProcedurallyGeneratedAnimals.BodyParts;

/// <summary>
/// Describes a body part of a creature.
/// </summary>
internal abstract class BodyPart
{
	protected Segment segment;
	protected Render render;
	protected Color color;

	/// <returns>The parent segment.</returns>
	public Segment Segment { get => segment; set => segment = value; }

	/// <returns>Where to render.</returns>
	public Render Render => render;

	/// <returns>Color of the bodyPart.</returns>
	public Color Color => color;

	/// <summary>
	/// Creates a <see cref="BodyPart"/> object.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <param name="render">Where to render.</param>
	/// <param name="color">Color of the bodyPart.</param>
	public BodyPart(Segment segment, Render render, Color color)
	{
		this.segment = segment;
		this.render = render;
		this.color = color;
	}

	/// <summary>
	/// Draws this body part instance.
	/// </summary>
	public abstract void Draw();
}