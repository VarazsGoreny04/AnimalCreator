namespace ProcedurallyGeneratedAnimals.Transformations;

/// <summary>
/// Describes a translation transformation.
/// </summary>
public class Translate : Transformation
{
	private readonly Point<double> value;

	/// <returns>The X and Y values of the translation.</returns>
	public Point<double> Value => value;

	/// <summary>
	/// Creates a <see cref="Translate"/> object.
	/// </summary>
	/// <param name="value">The X and Y values of the transformation.</param>
	public Translate(Point<double> value) => this.value = value;
}