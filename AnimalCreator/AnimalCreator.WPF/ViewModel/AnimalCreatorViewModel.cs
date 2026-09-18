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
		model.DrawEllipse += new EventHandler<EllipseEventArgs>((_, e) => shapes.Add(DrawEllipse(e)));
		model.DrawBezierLine += new EventHandler<BezierLineEventArgs>((_, e) => shapes.Add(DrawBezierLine(e)));
		model.ClearCanvas += new EventHandler((_, e) => { });

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

	private static Point[] MakeCurvePoints(Point[] points, double tension)
	{
		if (points.Length < 2)
			return points;

		double control_scale = tension / 0.5 * 0.175;

		List<Point> result_points = [points[0]];

		for (int i = 0; i < points.Length - 1; i++)
		{
			// Get the point and its neighbors.
			Point pt_before = points[Math.Max(i - 1, 0)];
			Point pt = points[i];
			Point pt_after = points[i + 1];
			Point pt_after2 = points[Math.Min(i + 2, points.Length - 1)];

			double dx1 = pt_after.X - pt_before.X;
			double dy1 = pt_after.Y - pt_before.Y;

			Point p1 = points[i];
			Point p4 = pt_after;

			double dx = pt_after.X - pt_before.X;
			double dy = pt_after.Y - pt_before.Y;
			Point p2 = new(
				pt.X + control_scale * dx,
				pt.Y + control_scale * dy
			);

			dx = pt_after2.X - pt.X;
			dy = pt_after2.Y - pt.Y;
			Point p3 = new(
				pt_after.X - control_scale * dx,
				pt_after.Y - control_scale * dy
			);

			// Save points p2, p3, and p4.
			result_points.Add(p2);
			result_points.Add(p3);
			result_points.Add(p4);
		}

		// Return the points.
		return [.. result_points];
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
				/*while (true)
				{
					if (pause)
						continue;*/
				Task task = Task.Delay(model.Data.WaitTime);

				Application.Current.Dispatcher.Invoke((Action)delegate
				{
					/*try
					{
						mouse = Mouse.GetPosition(canvas);
					}
					catch { }*/

					model.Draw(mouse.X, mouse.Y);
				});

				await task;
				/*}*/
			},
			tokenSource.Token
		);
	}
}