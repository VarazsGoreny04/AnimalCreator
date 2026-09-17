using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.Transformations;

namespace AnimalCreator.Model.EventArgs;

/// <summary>
/// Describes parameters of a Bézier line.
/// </summary>
public class BezierLineEventArgs : ProcedurallyGeneratedAnimals.EventArgs.BezierLineEventArgs
{
	/// <summary>
	/// Creates an BezierLineEventArgs object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="transformations">The transformations of the line.</param>
	/// <param name="color">The color of the line.</param>
	public BezierLineEventArgs(Point<double>[] points, Transformation[] transformations, Color color) : base(points, transformations, color) { }

	/// <summary>
	/// Creates an BezierLineEventArgs object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="transformations">The transformations of the line.</param>
	public BezierLineEventArgs(Point<double>[] points, Transformation[] transformations) : this(points, transformations, new Color(0, 0, 0, 0)) { }
}