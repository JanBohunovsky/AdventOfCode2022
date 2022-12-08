namespace Day08;

public record Vector2(int X, int Y)
{
	public static readonly Vector2 Up = new(0, -1);
	public static readonly Vector2 Down = new(0, 1);
	public static readonly Vector2 Left = new(-1, 0);
	public static readonly Vector2 Right = new(1, 0);

	public bool IsInBounds(int left, int up, int right, int down)
	{
		return X >= left && X <= right
			&& Y >= up && Y <= down;
	}

	public static Vector2 operator +(Vector2 left, Vector2 right)
	{
		return new Vector2(left.X + right.X, left.Y + right.Y);
	}
}