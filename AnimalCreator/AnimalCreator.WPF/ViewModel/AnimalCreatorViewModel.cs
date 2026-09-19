using AnimalCreator.Model;
using AnimalCreator.Model.EventArgs;
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
	private readonly ObservableCollection<ShapeData> shapes;
	private CancellationTokenSource tokenSource;
	private bool pause;
	private Point mouse;

	public int WindowWidth { get => windowWidth; set => windowWidth = value; }

	public int WindowHeight { get => windowHeight; set => windowHeight = value; }

	public AnimalCreatorModel Model => model;

	public ObservableCollection<ShapeData> Shapes => shapes;

	public AnimalCreatorViewModel()
	{
		shapes = [];

		model = new AnimalCreatorModel(400, 300);
		model.DrawEllipse += new EventHandler<EllipseEventArgs>((_, e) =>
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				shapes.Add(DrawEllipse(e));
			});
		});
		model.DrawBezierLine += new EventHandler<BezierLineEventArgs>((_, e) =>
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				shapes.Add(DrawBezierLine(e));
			});
		});
		model.ClearCanvas += new EventHandler((_, _) =>
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				shapes.Clear();
			});
		});

		tokenSource = new CancellationTokenSource();
		pause = false;
		mouse = new Point();

		DrawLoop();

		shapes.Add(
				new EllipseData(100, 100, new TranslateTransform(50, 0), new SolidColorBrush(Colors.Transparent)));
	}

	private static EllipseData DrawEllipse(EllipseEventArgs ellipseEventArgs)
	{
		Color color = new();
		if (ellipseEventArgs.Color is not null)
		{
			color.R = ellipseEventArgs.Color.R;
			color.G = ellipseEventArgs.Color.G;
			color.B = ellipseEventArgs.Color.B;
			color.A = ellipseEventArgs.Color.A;
		}

		TransformCollection transformations = [];

		if (ellipseEventArgs.Position is not null)
			transformations.Add(new TranslateTransform(ellipseEventArgs.Position.X, ellipseEventArgs.Position.Y));

		if (ellipseEventArgs.Angle is double angle)
			transformations.Add(new RotateTransform(angle));

		return new EllipseData(
			ellipseEventArgs.Dimensions.X,
			ellipseEventArgs.Dimensions.Y,
			new TransformGroup() { Children = transformations },
			new SolidColorBrush(color)
		);
	}

	// Legyen az ellipszis és a path is path, csak más geometriával
	// Négyzetes Bézier curve kell amihez meg kell írni a függvényt
	// Át kell írni a Segment és a BodyPart konstruktorát a JavaScript projektben is

	private static Point[] MakeCurvePoints(Point[] points)
	{
		static void AddOneCurve(Point prev, Point current, Point next, ref List<Point> result)
		{
			Vector v = (prev - next) / 4;

			result.Add(current + v);
			result.Add(current - v);
		}

		int length = points.Length;

		if (points.Length < 3)
			return points;

		Point prev = points[0];
		Point current = points[1];
		Point next;

		List<Point> result = new((length - 1) * 2) { prev };

		AddOneCurve(points[^1], prev, current, ref result);

		for (int i = 2; i < points.Length; ++i)
		{
			next = points[i];

			AddOneCurve(prev, current, next, ref result);

			prev = current;
			current = next;
		}

		AddOneCurve(prev, current, points[0], ref result);

		return [.. result];
	}

	private static PathData DrawBezierLine(BezierLineEventArgs bezierLineEventArgs)
	{
		Point[] points = [.. bezierLineEventArgs.Points.Select(x => new Point(x.X, x.Y))];
		IEnumerable<PathSegment> pathSegments = points.Select(x => new PolyBezierSegment(points[1..], true));

		PathFigure figures = new(points[0], pathSegments, true);

		Color color = new();
		if (bezierLineEventArgs.Color is not null)
		{
			color.R = bezierLineEventArgs.Color.R;
			color.G = bezierLineEventArgs.Color.G;
			color.B = bezierLineEventArgs.Color.B;
			color.A = bezierLineEventArgs.Color.A;
		}

		TransformCollection transformations = [];

		if (bezierLineEventArgs.Position is not null)
			transformations.Add(new TranslateTransform(bezierLineEventArgs.Position.X, bezierLineEventArgs.Position.Y));

		if (bezierLineEventArgs.Angle is double angle)
			transformations.Add(new RotateTransform(angle));

		return new PathData(
			new PathGeometry([figures]),
			new TransformGroup() { Children = transformations },
			new SolidColorBrush(color)
		);
	}

	private void DrawLoop()
	{
		tokenSource = new CancellationTokenSource();

		Task.Run(
			async () =>
			{
				while (true)
				{
					if (pause || Point.Distance(mouse, data.Animal.HeadPosition) < data.Animal.Speed))
						continue;

					await Task.Delay(model.Data.WaitTime);

					/*try
					{
						mouse = Mouse.GetPosition(canvas);
					}
					catch { }*/

					model.Draw(mouse.X, mouse.Y);
				}
			},
			tokenSource.Token
		);
	}
}