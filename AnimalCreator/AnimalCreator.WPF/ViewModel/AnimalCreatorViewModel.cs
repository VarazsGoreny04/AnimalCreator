using AnimalCreator.Model;
using ProcedurallyGeneratedAnimals.ShapeDataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AnimalCreator.WPF.ViewModel;

public class AnimalCreatorViewModel
{
	private int windowWidth = 800;
	private int windowHeight = 600;
	private readonly AnimalCreatorModel model;
	private readonly ObservableCollection<PathData> shapes;
	private bool pause;
	private Point mouse;
	private CancellationTokenSource? tokenSource;

	public int WindowWidth { get => windowWidth; set => windowWidth = value; }
	public int WindowHeight { get => windowHeight; set => windowHeight = value; }
	public AnimalCreatorModel Model => model;
	public ObservableCollection<PathData> Shapes => shapes;

	public DelegateCommand PauseCommand { get; }
	public DelegateCommand ChangeAnimalCommand { get; }

	public AnimalCreatorViewModel()
	{
		shapes = [];

		model = new AnimalCreatorModel(400, 300, 60);

		mouse = new Point(windowWidth / 2, windowHeight / 2);
		tokenSource = null;
		pause = false;

		PauseCommand = new DelegateCommand(_ => pause = !pause);
		ChangeAnimalCommand = new DelegateCommand(param =>
		{
			if (param?.ToString() is string text && uint.TryParse(text, out uint number))
			{
				model.Data.SelectIndex(number, windowWidth, WindowHeight);

				ShapeData[] shapeData = model.Draw(mouse.X, mouse.Y);

				shapes.Clear();
				foreach (PathData pathData in DataToPathsConverter(shapeData))
					shapes.Add(pathData);
			}
		});

		RunLoop();
	}

	private static PathData EllipsePathData(EllipseData ellipseData)
	{
		Color color = new();
		if (ellipseData.Color is ProcedurallyGeneratedAnimals.Color c)
		{
			color.R = c.R;
			color.G = c.G;
			color.B = c.B;
			color.A = c.A;
		}

		TransformCollection transformCollection = [];

		if (ellipseData.Rotation?.Center is ProcedurallyGeneratedAnimals.Point<double> point)
			transformCollection.Add(new TranslateTransform(point.X, point.Y));

		if (ellipseData.Rotation?.Angle is double angle)
			transformCollection.Add(new RotateTransform(angle));

		return new PathData(
			new EllipseGeometry(
				new Point(),
				ellipseData.Width / 2,
				ellipseData.Height / 2,
				new TransformGroup() { Children = transformCollection }
			),
			new TranslateTransform(ellipseData.Position.X, ellipseData.Position.Y),
			new SolidColorBrush(color)
		);
	}

	// Át kell írni a Segment és a BodyPart konstruktorát a JavaScript projektben is

	private static PathData BezierLinePathData(BezierLineData bezierLineData)
	{
		Point[] points = [.. BezierLineData.MakeCubicBezier(bezierLineData.Points).Select(x => new Point(x.X, x.Y))];

		PathFigure figures = new(points[0], [new PolyBezierSegment(points[1..], true)], false);

		Color color = new();
		if (bezierLineData.Color is ProcedurallyGeneratedAnimals.Color c)
		{
			color.R = c.R;
			color.G = c.G;
			color.B = c.B;
			color.A = c.A;
		}

		return new PathData(
			new PathGeometry(
				[figures],
				FillRule.Nonzero,
				bezierLineData.Angle is double angle ? new RotateTransform(angle) : Transform.Identity
			),
			bezierLineData.Position is ProcedurallyGeneratedAnimals.Point<double> point ? new TranslateTransform(point.X, point.Y) : Transform.Identity,
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
				EllipseData s => EllipsePathData(s),
				BezierLineData s => BezierLinePathData(s),
				_ => throw new NotImplementedException($"The conversion from {nameof(ShapeData)} to {nameof(PathData)} must be implemented for every type!")
			};

			result.Add(pathData);
		}

		return [.. result];
	}

	private void RunLoop()
	{
		tokenSource = tokenSource is null ? new CancellationTokenSource() : throw new ArgumentException("The previously started task is still running!");

		async void Loop()
		{
			Task delay;
			ProcedurallyGeneratedAnimals.Point<double> head;

			while (!tokenSource.IsCancellationRequested)
			{
				delay = Task.Delay(model.Data.WaitTime);

				head = model.Data.Animal.HeadPosition;

				if (!pause && (uint)Math.Sqrt(Math.Pow(mouse.X - head.X, 2) + Math.Pow(mouse.Y - head.Y, 2)) >= model.Data.Animal.Speed)
				{
					ShapeData[] shapes = model.Draw(mouse.X, mouse.Y);

					Application.Current?.Dispatcher.Invoke(delegate
					{
						this.shapes.Clear();
						foreach (PathData pathData in DataToPathsConverter(shapes))
							this.shapes.Add(pathData);
					});
				}

				await delay;
			}

			tokenSource = null;
		}

		_ = Task.Run(Loop);
	}

	public void OnMouseMove(object sender, MouseEventArgs e)
	{
		if (sender is UIElement element)
			mouse = e.GetPosition(element);
	}

	public void OnClosingWindow(object? sender, CancelEventArgs e)
	{
		if (e.Cancel)
			tokenSource?.Cancel();
	}
}