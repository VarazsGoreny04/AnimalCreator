using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.Transformations;

namespace AnimalCreator.Model.EventArgs;

/// <summary>
/// Describes parameters of an ellipse.
/// </summary>
public class EllipseEventArgs : ProcedurallyGeneratedAnimals.EventArgs.EllipseEventArgs
{
	/// <summary>
	/// Creates an EllipseEventArgs object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="transforms">The transformations of the ellipse.</param>
	/// <param name="color">The color of the ellipse.</param>
	public EllipseEventArgs(Point<int> dimensions, Transformation[] transforms, Color color) : base(dimensions, transforms, color) { }

	/// <summary>
	/// Creates an EllipseEventArgs object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="transforms">The transformations of the ellipse.</param>
	public EllipseEventArgs(Point<int> dimensions, Transformation[] transforms) : this(dimensions, transforms, new Color(0, 0, 0, 0)) { }
}