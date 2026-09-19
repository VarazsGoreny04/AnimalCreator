using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;

namespace AnimalCreator.Model.EventArgs;

/// <summary>
/// Describes a Bézier line.
/// </summary>
public class BezierLineEventArgs : BezierLineData, IShapeEventArgs
{
	/// <summary>
	/// Creates an BezierLineEventArgs object.
	/// </summary>
	/// <param name="points">The points of the line.</param>
	/// <param name="position">The origin position of the line.</param>
	/// <param name="angle">The angle of the line.</param>
	/// <param name="color">The color of the line.</param>
	public BezierLineEventArgs(Point<double>[] points, Point<double>? position, double? angle, Color? color) : base(points,	position, angle, color) { }

	/// <summary>
	/// Creates an BezierLineEventArgs object.
	/// </summary>
	/// <param name="eventArgs">The <see cref="ProcedurallyGeneratedAnimals.EventArgs.BezierLineEventArgs"/> to copy values from.</param>
	public BezierLineEventArgs(BezierLineData eventArgs)
		: this(eventArgs.Points, eventArgs.Position, eventArgs.Angle, eventArgs.Color) { }
}