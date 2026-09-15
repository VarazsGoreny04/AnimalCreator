using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnimalCreator.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		// Add a "Hello World!" text element to the Canvas
		Polyline txt1 = new()
		{
			Points = [
				new Point(25,25),
				new Point(0,50),
				new Point(25,75),
				new Point(50,50),
				new Point(25,25),
				new Point(25,0),
			],
			Stroke = new SolidColorBrush(Colors.Blue),
			StrokeThickness = 10d,
			RenderTransformOrigin = new Point(0.5, 0.5),
			RenderTransform = new TransformGroup()
			{
				Children = new TransformCollection([
					new RotateTransform(45),
					new ScaleTransform(2, 2)
				])
			}
		};
		Canvas.SetTop(txt1, 100);
		Canvas.SetLeft(txt1, 100);
		MainCanvas.Children.Add(txt1);
	}
}