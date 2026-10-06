namespace ProcedurallyGeneratedAnimals;

/// <summary>
/// Describes a color value.
/// </summary>
public sealed class Color
{
	#region Fields

	private readonly byte r;
	private readonly byte g;
	private readonly byte b;
	private readonly byte a;

	#endregion

	#region Properties

	/// <returns>The amount of red from 0 to 255.</returns>
	public byte R => r;

	/// <returns>The amount of green from 0 to 255.</returns>
	public byte G => g;

	/// <returns>The amount of blue from 0 to 255.</returns>
	public byte B => b;

	/// <returns>The alpha value from 0 to 255.</returns>
	public byte A => a;

	#endregion

	#region Constructors

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

	#endregion
}