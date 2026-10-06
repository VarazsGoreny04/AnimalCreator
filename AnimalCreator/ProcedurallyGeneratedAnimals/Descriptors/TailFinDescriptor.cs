using ProcedurallyGeneratedAnimals.BodyParts;

namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes a fin.
/// </summary>
public class TailFinDescriptor : BodyPartDescriptor
{
	#region Fields

	protected int[] segmentDistances;

	#endregion

	#region Properties

	/// <returns>The distances of the segments of the fin.</returns>
	public int[] SegmentDistances => segmentDistances;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a <see cref="TailFinDescriptor"/> object.
	/// </summary>
	/// <param name="segmentDistances">The distances of the segments of the fin.</param>
	/// <param name="color">The color of the fin.</param>
	/// <param name="render">Where to render.</param>
	public TailFinDescriptor(int[] segmentDistances, Color color, Render render = Render.Bottom) : base(render, color) => this.segmentDistances = segmentDistances;

	#endregion

	#region Internal methods

	/// <summary>
	/// Creates a <see cref="TailFin"/> object by this descriptor.
	/// </summary>
	/// <param name="segment">The parent segment.</param>
	/// <returns>The <see cref="TailFin"/> object.</returns>
	internal override BodyPart Create(Segment segment) => new TailFin(segment, render, segmentDistances, color);

	#endregion
}