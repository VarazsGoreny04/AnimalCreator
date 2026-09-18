using AnimalCreator.Model.EventArgs;
using AnimalCreator.Persistence;
using ProcedurallyGeneratedAnimals;
using System;

namespace AnimalCreator.Model;

public sealed class AnimalCreatorModel
{
	private readonly AnimalCreatorData data;

	public AnimalCreatorData Data => data;

	public event EventHandler<EllipseEventArgs>? DrawEllipse;
	public event EventHandler<BezierLineEventArgs>? DrawBezierLine;
	public event EventHandler? ClearCanvas;

	public AnimalCreatorModel(int windowWidth, int windowHeight)
	{
		data = new AnimalCreatorData(windowWidth, windowHeight);

		Animal.DrawEllipse += new EventHandler<ProcedurallyGeneratedAnimals.EventArgs.EllipseEventArgs>((_, e) => OnDrawEllipse(e));
		Animal.DrawBezierLine += new EventHandler<ProcedurallyGeneratedAnimals.EventArgs.BezierLineEventArgs>((_, e) => OnDrawBezierLine(e));
	}

	private void OnDrawEllipse(ProcedurallyGeneratedAnimals.EventArgs.EllipseEventArgs ellipse)
	{
		DrawEllipse?.Invoke(this, new EllipseEventArgs(ellipse));
	}

	private void OnDrawBezierLine(ProcedurallyGeneratedAnimals.EventArgs.BezierLineEventArgs bezierLine)
	{
		DrawBezierLine?.Invoke(this, new BezierLineEventArgs(bezierLine));
	}

	private void OnClearCanvas()
	{
		ClearCanvas?.Invoke(this, System.EventArgs.Empty);
	}

	public void Draw(double x, double y)
	{
		OnClearCanvas();

		Point<double> mouse = new(x, y);

		if (Point.Distance(mouse, data.Animal.HeadPosition) < data.Animal.Speed)
			return;

		data.Animal.Step(mouse);
		data.Animal.Draw();
	}
}