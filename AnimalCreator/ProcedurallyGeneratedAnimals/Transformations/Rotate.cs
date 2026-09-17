namespace ProcedurallyGeneratedAnimals.Transformations;

/// <summary>
/// Describes a rotation transformation.
/// </summary>
public class Rotate : Transformation
{
	private readonly double angle;

	/// <returns>The angle of the rotation.</returns>
	public double Angle => angle;

	/// <summary>
	/// Creates a <see cref="Rotate"/> object.
	/// </summary>
	/// <param name="angle">The angle of the rotation.</param>
	public Rotate(double angle) => this.angle = angle;
}
