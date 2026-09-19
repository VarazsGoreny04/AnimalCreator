using AnimalCreator.Model.EventArgs;
using AnimalCreator.Persistence;
using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System;
using System.Collections.Generic;

namespace AnimalCreator.Model;

public sealed class AnimalCreatorModel
{
	private readonly AnimalCreatorData data;

	public AnimalCreatorData Data => data;

	public event EventHandler<EllipseEventArgs>? DrawEllipse;
	public event EventHandler<BezierLineEventArgs>? DrawBezierLine;
	public event EventHandler? ClearCanvas;

	public AnimalCreatorModel(int windowWidth, int windowHeight) => data = new AnimalCreatorData(windowWidth, windowHeight);

	public IShapeEventArgs[] Draw(double x, double y)
	{
		Point<double> mouse = new(x, y);

		data.Animal.Step(mouse);
		ShapeData[] shapes = data.Animal.Draw();

		List<IShapeEventArgs> result = new(shapes.Length);
		IShapeEventArgs shapeEventArgs;

		foreach (ShapeData shape in shapes)
		{
			shapeEventArgs = shape switch
			{
				EllipseData s => new EllipseEventArgs(s),
				BezierLineData s => new BezierLineEventArgs(s),
				_ => throw new NotImplementedException($"The conversion from {nameof(ShapeData)} to {nameof(IShapeEventArgs)} must be implemented for every type!")
			};

			result.Add(shapeEventArgs);
		}

		return [.. result];
	}
}