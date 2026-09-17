namespace ProcedurallyGeneratedAnimals.Transformations;

/// <summary>
/// Describes a scaling transformation.
/// </summary>
public class Scale : Transformation
{
	private readonly Point<double> value;

	/// <returns>The X and Y values of the scaling.</returns>
	public Point<double> Value => value;

	/// <summary>
	/// Creates a <see cref="Scale"/> object.
	/// </summary>
	/// <param name="value">The X and Y values of the scaling.</param>
	public Scale(Point<double> value) => this.value = value;
}