namespace ProcedurallyGeneratedAnimals.ShapeDataTypes;

/// <summary>
/// Describes a shape.
/// </summary>
public abstract class ShapeData
{
	#region Fields

	protected Color? color;

	#endregion

	#region Properties

	/// <returns>The color of the shape.</returns>
	public Color? Color => color;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a <see cref="ShapeData"/> object.
	/// </summary>
	/// <param name="color">The color of the shape.</param>
	protected ShapeData(Color? color) => this.color = color;

	#endregion
}