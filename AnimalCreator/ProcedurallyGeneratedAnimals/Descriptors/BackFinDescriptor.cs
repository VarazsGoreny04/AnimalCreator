using ProcedurallyGeneratedAnimals.BodyParts;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a fin.
/// </summary>
public class BackFinDescriptor : BodyPartDescriptor
{
	#region Fields

	protected int lengthInSegments;

	#endregion

	#region Properties

	/// <returns>The number of segments the fin will go through.</returns>
	public int LengthInSegments => lengthInSegments;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a <see cref="BackFinDescriptor"/> object.
	/// <param name="lengthInSegments">The number of segments the fin will go through.</param>
	/// <param name="color">The color of the fin.</param>
	/// <param name="render">Where to render.</param>
	public BackFinDescriptor(int lengthInSegments, Color color, Render render = Render.Top) : base(render, color) => this.lengthInSegments = lengthInSegments;

	#endregion

	#region Internal methods

	/// <summary>
	/// Creates a <see cref="BackFin"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="BackFin"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new BackFin(segment, render, lengthInSegments, color);

	#endregion
}