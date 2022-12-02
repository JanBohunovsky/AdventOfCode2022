using System.Diagnostics;

internal enum Shape
{
	Rock = 1,
	Paper = 2,
	Scissors = 3,
}

internal static class ShapeHelper
{
	internal static bool IsBeatenBy(this Shape shape, Shape other)
	{
		return GetWinningShape(shape) == other;
	}

	/// <summary>
	/// Returns a <see cref="Shape"/> that beats this <paramref name="shape"/>.
	/// </summary>
	internal static Shape GetWinningShape(Shape shape)
	{
		return shape switch
		{
			Shape.Rock => Shape.Paper,
			Shape.Paper => Shape.Scissors,
			Shape.Scissors => Shape.Rock,
			_ => throw new UnreachableException(),
		};
	}

	/// <summary>
	/// Returns a <see cref="Shape"/> that is beaten by this <paramref name="shape"/>.
	/// </summary>
	internal static Shape GetLosingShape(Shape shape)
	{
		return shape switch
		{
			Shape.Rock => Shape.Scissors,
			Shape.Paper => Shape.Rock,
			Shape.Scissors => Shape.Paper,
			_ => throw new UnreachableException(),
		};
	}
}