using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;

namespace AnimalCreator.Model.EventArgs;

/// <summary>
/// Describes an ellipse.
/// </summary>
public class EllipseEventArgs : EllipseData, IShapeEventArgs
{
	/// <summary>
	/// Creates an EllipseEventArgs object.
	/// </summary>
	/// <param name="dimensions">The dimensions of the ellipse.</param>
	/// <param name="position">The position of the ellipse.</param>
	/// <param name="angle">The angle of the ellipse.</param>
	/// <param name="color">The color of the ellipse.</param>
	public EllipseEventArgs(Point<int> dimensions, Point<double> position, double? angle, Color? color) : base(dimensions, position, angle, color) { }

	/// <summary>
	/// Creates an EllipseEventArgs object.
	/// </summary>
	/// <param name="eventArgs">The <see cref="ProcedurallyGeneratedAnimals.EventArgs.BezierLineEventArgs"/> to copy values from.</param>
	public EllipseEventArgs(EllipseData eventArgs)
		: this(eventArgs.Dimensions, eventArgs.Position, eventArgs.Angle, eventArgs.Color) { }
}