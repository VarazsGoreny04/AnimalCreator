using AnimalCreator.Model.EventArgs;
using AnimalCreator.Persistence;
/*using ProcedurallyGeneratedAnimals;
using ProcedurallyGeneratedAnimals.Transformations;*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AnimalCreator.Model;

public sealed class AnimalCreatorModel
{
	private readonly AnimalCreatorData data;

	public AnimalCreatorData Data => data;

	public static event EventHandler<EllipseEventArgs>? DrawEllipse;
	public static event EventHandler<BezierLineEventArgs>? DrawBezierLine;

	public AnimalCreatorModel() => data = new AnimalCreatorData();

	public void DrawLoop(CancellationTokenSource tokenSource)
	{
		Task.Run(() =>
		{
			while (!tokenSource.IsCancellationRequested)
			{
				data.Animal.Draw();

				Thread.Sleep(data.WaitTime);
			}
		}, tokenSource.Token);
	}

	/*internal static void OnDrawEllipse(Point<int> dimensions, Transformation[] transforms, Color color)
	{
		DrawEllipse?.Invoke(null, new EllipseEventArgs(dimensions, transforms, color));
	}

	internal static void OnDrawBezierLine(Point<double>[] points, Transformation[] transformations)
	{
		DrawBezierLine?.Invoke(null, new BezierLineEventArgs(points, transformations));
	}

	internal static void OnDrawBezierLine(Point<double>[] points, Transformation[] transformations, Color color)
	{
		DrawBezierLine?.Invoke(null, new BezierLineEventArgs(points, transformations, color));
	}*/
}