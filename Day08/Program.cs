var input = File.ReadAllLines("Data/input.txt");
var heightMap = HeightMap.Parse(input);

var directions = new[]
{
	Vector2.Up,
	Vector2.Down,
	Vector2.Left,
	Vector2.Right,
};

var count = heightMap.Size * 4 - 4;
var highestScenicScore = 0;

for (int y = 1; y < heightMap.Size - 1; y++)
{
	for (int x = 1; x < heightMap.Size - 1; x++)
	{
		var position = new Vector2(x, y);

		// Part 1
		if (directions.Any(d => heightMap.IsTreeVisibleFromOutside(position, d)))
		{
			count++;
		}

		// Part 2
		// Scenic score is calculated by multiplying viewing distances together,
		// this is also why we ignore the borders since one of the directions would give us 0.
		var scenicScore = directions.Aggregate(1, (score, direction) => score * heightMap.GetTreeViewingDistance(position, direction));
		if (scenicScore > highestScenicScore)
		{
			highestScenicScore = scenicScore;
		}
	}
}

Console.WriteLine($"Part 1: {count}");
Console.WriteLine($"Part 2: {highestScenicScore}");