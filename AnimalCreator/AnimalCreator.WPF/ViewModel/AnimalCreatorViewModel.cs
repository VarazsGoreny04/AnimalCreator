using AnimalCreator.Model;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AnimalCreator.WPF.ViewModel;

public class AnimalCreatorViewModel
{
	private int windowWidth = 800;
	private int windowHeight = 600;
	private readonly AnimalCreatorModel model;
	private readonly ObservableCollection<PathData> shapes;
	private CancellationTokenSource tokenSource;
	private bool pause;
	private Point mouse;

	public int WindowWidth { get => windowWidth; set => windowWidth = value; }

	public int WindowHeight { get => windowHeight; set => windowHeight = value; }

	public AnimalCreatorModel Model => model;

	public ObservableCollection<PathData> Shapes => shapes;

	public AnimalCreatorViewModel()
	{
		shapes = [];

		model = new AnimalCreatorModel(400, 300, 60);

		tokenSource = new CancellationTokenSource();
		pause = false;
		mouse = new Point();

		DrawLoop();
	}

	private static PathData DrawEllipse(EllipseData ellipseData)
	{
		Color color = new();
		if (ellipseData.Color is not null)
		{
			color.R = ellipseData.Color.R;
			color.G = ellipseData.Color.G;
			color.B = ellipseData.Color.B;
			color.A = ellipseData.Color.A;
		}

		return new PathData(
			new EllipseGeometry(new Point(ellipseData.Position.X, ellipseData.Position.Y), ellipseData.Width, ellipseData.Height),
			ellipseData.Angle is double angle ? new RotateTransform(angle) : Transform.Identity,
			new SolidColorBrush(color)
		);
	}

	// Át kell írni a Segment és a BodyPart konstruktorát a JavaScript projektben is

	private static PathData DrawBezierLine(BezierLineData bezierLineData)
	{
		Point[] points = [.. BezierLineData.MakeCubicBezier(bezierLineData.Points).Select(x => new Point(x.X, x.Y))];

		PathFigure figures = new(points[0], [new PolyBezierSegment(points[1..], true)], true);

		Color color = new();
		if (bezierLineData.Color is not null)
		{
			color.R = bezierLineData.Color.R;
			color.G = bezierLineData.Color.G;
			color.B = bezierLineData.Color.B;
			color.A = bezierLineData.Color.A;
		}

		TransformCollection transformations = [];

		if (bezierLineData.Position is not null)
			transformations.Add(new TranslateTransform(bezierLineData.Position.X, bezierLineData.Position.Y));

		if (bezierLineData.Angle is double angle)
			transformations.Add(new RotateTransform(angle));

		return new PathData(
			new PathGeometry([figures]),
			new TransformGroup() { Children = transformations },
			new SolidColorBrush(color)
		);
	}

	private static PathData[] DataToPathsConverter(ShapeData[] data)
	{
		List<PathData> result = new(data.Length);
		PathData pathData;

		foreach (ShapeData shape in data)
		{
			pathData = shape switch
			{
				EllipseData s => DrawEllipse(s),
				BezierLineData s => DrawBezierLine(s),
				_ => throw new NotImplementedException($"The conversion from {nameof(ShapeData)} to {nameof(PathData)} must be implemented for every type!")
			};

			result.Add(pathData);
		}

		return [.. result];
	}

	private void DrawLoop()
	{
		tokenSource = new CancellationTokenSource();

		Task.Run(
			async () =>
			{
				while (true)
				{
					/*if (pause || Point.Distance(mouse, data.Animal.HeadPosition) < data.Animal.Speed))
						continue;*/

					Task delay = Task.Delay(model.Data.WaitTime);

					/*try
					{
						mouse = Mouse.GetPosition(canvas);
					}
					catch { }*/

					ShapeData[] shapes = model.Draw(mouse.X, mouse.Y);

					try
					{
						Application.Current.Dispatcher.Invoke(delegate
						{
							this.shapes.Clear();

							foreach (PathData pathData in DataToPathsConverter(shapes))
								this.shapes.Add(pathData);
						});
					}
					catch { }

					await delay;
				}
			},
			tokenSource.Token
		);
	}
}