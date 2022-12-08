namespace Day08;

public class HeightMap
{
	public int Size { get; }

	private readonly int[][] _data;

	private HeightMap(int[][] data)
	{
		_data = data;
		Size = data.Length;
	}

	public int GetTreeHeight(Vector2 position)
	{
		if (!IsInBounds(position))
		{
			throw new ArgumentOutOfRangeException(nameof(position));
		}

		return _data[position.Y][position.X];
	}

	public bool IsInBounds(Vector2 position)
	{
		return position.IsInBounds(0, 0, Size - 1, Size - 1);
	}

	public bool IsTreeVisibleFromOutside(Vector2 position, Vector2 direction)
	{
		var treeHeight = GetTreeHeight(position);

		var currentPosition = position + direction;
		while (IsInBounds(currentPosition))
		{
			if (GetTreeHeight(currentPosition) >= treeHeight)
			{
				return false;
			}

			currentPosition += direction;
		}

		return true;
	}

	public int GetTreeViewingDistance(Vector2 position, Vector2 direction)
	{
		var treeHeight = GetTreeHeight(position);

		var distance = 0;
		var currentPosition = position + direction;

		while (IsInBounds(currentPosition))
		{
			distance++;

			if (GetTreeHeight(currentPosition) >= treeHeight)
			{
				break;
			}

			currentPosition += direction;
		}

		return distance;
	}

	public static HeightMap Parse(string[] input)
	{
		var data = input.Select(s => s
				.Select(c => int.Parse(c.ToString()))
				.ToArray())
			.ToArray();

		return new HeightMap(data);
	}
}