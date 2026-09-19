namespace ProcedurallyGeneratedAnimals.ShapeDataTypes;

/// <summary>
/// Describes a shape.
/// </summary>
public abstract class ShapeData
{
	protected Color? color;

	/// <returns>The color of the shape.</returns>
	public Color? Color => color;

	/// <summary>
	/// Creates a <see cref="ShapeData"/> object.
	/// </summary>
	/// <param name="color">The color of the shape.</param>
	protected ShapeData(Color? color) => this.color = color;
}