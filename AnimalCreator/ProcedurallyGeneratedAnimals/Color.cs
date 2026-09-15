namespace ProcedurallyGeneratedAnimals;

/// <summary>
/// Describes a color value.
/// </summary>
public class Color
{
	private byte r;
	private byte g;
	private byte b;
	private byte a;

	/// <returns>The amount of red from 0 to 255.</returns>
	public byte R { get => r; set => r = value; }

	/// <returns>The amount of green from 0 to 255.</returns>
	public byte G { get => g; set => g = value; }

	/// <returns>The amount of blue from 0 to 255.</returns>
	public byte B { get => b; set => b = value; }

	/// <returns>The alpha value from 0 to 255.</returns>
	public byte A { get => a; set => a = value; }

	/// <summary>Creates a <see cref="Color"/> object.</summary>
	/// <param name="r">The amount of red from 0 to 255.</param>
	/// <param name="g">The amount of green from 0 to 255.</param>
	/// <param name="b">The amount of blue from 0 to 255.</param>
	/// <param name="a">The alpha value from 0 to 255.</param>
	public Color(byte r, byte g, byte b, byte a = 255)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
	}
}