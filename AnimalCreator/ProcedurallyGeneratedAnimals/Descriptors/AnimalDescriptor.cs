namespace ProcedurallyGeneratedAnimals.Descriptors;

/// <summary>
/// Describes an animal.
/// </summary>
public class AnimalDescriptor
{
	#region Fields

	protected SegmentDescriptor[] segmentDescriptors;
	protected Color color;
	protected uint speed;

	#endregion

	#region Properties

	/// <returns>The segments of the animal.</returns>
	public SegmentDescriptor[] SegmentDescriptors => segmentDescriptors;

	/// <returns>The color of the body.</returns>
	public Color Color => color;

	/// <returns>The speed of the animal.</returns>
	public uint Speed => speed;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates an <see cref="AnimalDescriptor"/> object.
	/// </summary>
	/// <param name="segmentDescriptors">The segments of the animal.</param>
	/// <param name="color">The color of the body.</param>
	/// <param name="speed">The speed of the animal.</param>
	public AnimalDescriptor(SegmentDescriptor[] segmentDescriptors, Color color, uint speed)
	{
		this.segmentDescriptors = segmentDescriptors;
		this.color = color;
		this.speed = speed;
	}

	#endregion

	#region Public methods

	/// <summary>
	/// Creates an <see cref="Animal"/> object by this descriptor.
	/// </summary>
	/// <returns>The <see cref="Animal"/> object.</returns>
	public Animal Create(Point<int> headPosition) => new(headPosition, segmentDescriptors, color, speed);

	#endregion
}