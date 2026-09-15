using System;
using System.Numerics;

namespace ProcedurallyGeneratedAnimals;

/// <summary>
/// Describes a point in the 2D space.
/// </summary>
/// <typeparam name="T">The type of the X and Y values.</typeparam>
public sealed class Point<T> where T : INumber<T>
{
	private readonly T x;
	private readonly T y;

	/// <returns>The X value.</returns>
	public T X => x;

	/// <returns>The Y value.</returns>
	public T Y => y;

	/// <summary>
	/// Creates a <see cref="Point{T}"/> object.
	/// </summary>
	/// <param name="x">The X value.</param>
	/// <param name="y">The Y value.</param>
	public Point(T x, T y)
	{
		this.x = x;
		this.y = y;
	}
}

/// <summary>
/// Describes operations for <see cref="Point{T}"/>s.
/// </summary>
public static class Point
{
	/// <summary>
	/// Converts the fields of the given <see cref="Point{int}"/> form <see cref="int"/> to <see cref="double"/>.
	/// </summary>
	/// <param name="p">The given <see cref="Point{int}"/>.</param>
	/// <returns>The converted <see cref="Point{double}"/>.</returns>
	public static Point<double> IntToDouble(Point<int> p) => new(p.X, p.Y);

	/// <summary>
	/// Converts the given <see cref="Point{double}"/> form <see cref="double"/> to <see cref="int"/>.
	/// </summary>
	/// <param name="p">The given <see cref="Point{double}"/>.</param>
	/// <returns>The converted <see cref="Point{int}"/>.</returns>
	public static Point<int> DoubleToInt(Point<double> p) => new((int)p.X, (int)p.Y);

	/// <summary>
	/// Adds two <see cref="Point{T}"/>s.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>s.</typeparam>
	/// <param name="a">The first <see cref="Point{T}"/>.</param>
	/// <param name="b">The second <see cref="Point{T}"/>.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> Add<T>(Point<T> a, Point<T> b) where T : INumber<T> => new(a.X + b.X, a.Y + b.Y);

	/// <summary>
	/// Subtracts two <see cref="Point{T}"/>s.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>s.</typeparam>
	/// <param name="a">The first <see cref="Point{T}"/>.</param>
	/// <param name="b">The second <see cref="Point{T}"/>.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> Subtract<T>(Point<T> a, Point<T> b) where T : INumber<T> => new(a.X - b.X, a.Y - b.Y);

	/// <summary>
	/// Scales a vector by a number.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>.</typeparam>
	/// <param name="v">The vector.</param>
	/// <param name="s">The number.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> Multiply<T>(Point<T> v, T s) where T : INumber<T> => new(v.X * s, v.Y * s);

	/// <summary>
	/// Divides a vector by a number.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>.</typeparam>
	/// <param name="v">The vector.</param>
	/// <param name="s">The number.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> Divide<T>(Point<T> v, T s) where T : INumber<T> => new(v.X / s, v.Y / s);

	/// <summary>
	/// Gets the magnitude of the given vector.
	/// </summary>
	/// <param name="v">The vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static double Magnitude(Point<double> v) => Math.Sqrt(Convert.ToDouble(v.X * v.X + v.Y * v.Y));

	/// <summary>
	/// Gets the distance of the two <see cref="Point{double}"/>s.
	/// </summary>
	/// <param name="a">The first <see cref="Point{double}"/>.</param>
	/// <param name="b">The second <see cref="Point{double}"/>.</param>
	/// <returns>The result of the calculation.</returns>
	public static double Distance(Point<double> a, Point<double> b) => Magnitude(Subtract(a, b));

	/// <summary>
	/// Normalizes the given vector.
	/// </summary>
	/// <param name="v">The vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<double> Normalize(Point<double> v) => Divide(v, Magnitude(v));

	/// <summary>
	/// Scales a vector to a length.
	/// </summary>
	/// <param name="v">The vector.</param>
	/// <param name="s">The length.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<double> Scale(Point<double> v, double s) => Multiply(Normalize(v), s);

	/// <summary>
	/// Gets the left normal vector of the given vector.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>.</typeparam>
	/// <param name="v">The vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> NormalLeft<T>(Point<T> v) where T : INumber<T> => new(-v.Y, v.X);

	/// <summary>
	/// Gets the right normal vector of the given vector.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>.</typeparam>
	/// <param name="v">The vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> NormalRight<T>(Point<T> v) where T : INumber<T> => new(v.Y, -v.X);

	/// <summary>
	/// Reverses the given vector.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>.</typeparam>
	/// <param name="v">The vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> Reverse<T>(Point<T> v) where T : INumber<T> => new(-v.X, -v.Y);

	/// <summary>
	/// Rotates the given vector by a radian value.
	/// </summary>
	/// <param name="v">The vector.</param>
	/// <param name="radian">The radian value.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<double> RotateRadian(Point<double> v, double radian)
	{
		double sinR = Math.Sin(radian);
		double cosR = Math.Cos(radian);

		return new Point<double>(cosR * v.X - sinR * v.Y, sinR * v.X + cosR * v.Y);
	}

	/// <summary>
	/// Rotates the given vector by a degree value.
	/// </summary>
	/// <param name="v">The vector.</param>
	/// <param name="degree">The degree value.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<double> RotateDegree(Point<double> v, double degree) => RotateRadian(v, double.DegreesToRadians(degree));

	/// <summary>
	/// Gets the dot product of the given vectors.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>s.</typeparam>
	/// <param name="v1">The first vector.</param>
	/// <param name="v2">The second vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static T Dot<T>(Point<T> v1, Point<T> v2) where T : INumber<T> => v1.X * v2.X + v1.Y * v2.Y;

	/// <summary>
	/// Projects a vector to another.
	/// </summary>
	/// <typeparam name="T">The type of the fields in the <see cref="Point{T}"/>s.</typeparam>
	/// <param name="v1">The vector to project.</param>
	/// <param name="v2">The line of the projection.</param>
	/// <returns>The result of the calculation.</returns>
	public static Point<T> Project<T>(Point<T> v1, Point<T> v2) where T : INumber<T> => Multiply(v2, Dot(v1, v2) / Dot(v1, v2));

	/// <summary>
	/// Gets the cosine of two vectors.
	/// </summary>
	/// <param name="v1">The first vector.</param>
	/// <param name="v2">The second vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static double CosOfVectors(Point<double> v1, Point<double> v2) => Dot(v1, v2) / (Magnitude(v1) * Magnitude(v2));

	/// <summary>
	/// Gets the sine of two vectors.
	/// </summary>
	/// <param name="v1">The first vector.</param>
	/// <param name="v2">The second vector.</param>
	/// <returns>The result of the calculation.</returns>
	public static double SinOfVectors(Point<double> v1, Point<double> v2)
	{
		v1 = NormalLeft(v1);
		return CosOfVectors(v1, v2);
	}

	/// <summary>
	/// Gets the cosine of three <see cref="Point{double}"/>s.
	/// </summary>
	/// <param name="a">The first <see cref="Point{double}"/>.</param>
	/// <param name="b">The middle <see cref="Point{double}"/>.</param>
	/// <param name="c">The last <see cref="Point{double}"/>.</param>
	/// <returns>The result of the calculation.</returns>
	public static double CosOfPoints(Point<double> a, Point<double> b, Point<double> c)
	{
		Point<double> v1 = Subtract(a, b);
		Point<double> v2 = Subtract(c, b);

		return CosOfVectors(v1, v2);
	}

	/// <summary>
	/// Gets the sine of three <see cref="Point{double}"/>s.
	/// </summary>
	/// <param name="a">The first <see cref="Point{double}"/>.</param>
	/// <param name="b">The middle <see cref="Point{double}"/>.</param>
	/// <param name="c">The last <see cref="Point{double}"/>.</param>
	/// <returns>The result of the calculation.</returns>
	public static double SinOfPoints(Point<double> a, Point<double> b, Point<double> c)
	{
		Point<double> v1 = Subtract(a, b);
		Point<double> v2 = Subtract(c, b);

		return SinOfVectors(v1, v2);
	}

	/// <summary>
	/// Gets the angle of a vector to the X base line.
	/// </summary>
	/// <param name="v">The vector.</param>
	/// <returns>The angle in degrees.</returns>
	public static double AngleOfVector(Point<double> v) => double.RadiansToDegrees(Math.Atan2(v.Y, v.X));

	/// <summary>
	/// Gets the angle of two vectors.
	/// </summary>
	/// <param name="v1">The first vector.</param>
	/// <param name="v2">The second vector.</param>
	/// <returns>The angle in degrees.</returns>
	public static double AngleOfVectors(Point<double> v1, Point<double> v2)
	{
		double angle = double.RadiansToDegrees(Math.Atan2(v2.Y, v2.X) - Math.Atan2(v1.Y, v1.X));

		return angle > 180 ? angle - 360 : angle < -180 ? angle + 360 : angle;
	}

	/// <summary>
	/// Gets the angle of three <see cref="Point{double}"/>s.
	/// </summary>
	/// <param name="a">The first <see cref="Point{double}"/>.</param>
	/// <param name="b">The middle <see cref="Point{double}"/>.</param>
	/// <param name="c">The last <see cref="Point{double}"/>.</param>
	/// <returns>The angle in degrees.</returns>
	public static double AngleOfPoints(Point<double> a, Point<double> b, Point<double> c)
	{
		Point<double> v1 = Subtract(a, b);
		Point<double> v2 = Subtract(c, b);

		return AngleOfVectors(v1, v2);
	}

	/// <summary>
	/// Restricts the angle a given vector.
	/// </summary>
	/// <param name="baseVector">The base vector.</param>
	/// <param name="directionVector">The vector to restrict.</param>
	/// <param name="maxAngle">The maximum angle in degrees.</param>
	/// <param name="minAngle">The minimum angle in degrees.</param>
	/// <returns>The restricted vector.</returns>
	public static Point<double> RestrictAngleOfRotation(Point<double> baseVector, Point<double> directionVector, double maxAngle, double minAngle)
	{
		double angleBetween = AngleOfVectors(baseVector, directionVector);

		if (minAngle < angleBetween && angleBetween < maxAngle)
			return directionVector;

		double toRotate = (minAngle < angleBetween ? maxAngle : minAngle) - angleBetween;

		return RotateDegree(directionVector, toRotate);
	}
}