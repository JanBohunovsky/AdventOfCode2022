var input = File.ReadAllText("Data/input.txt")
	.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
	.Select(g => g
		.Split('\n', StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse)
		.ToArray())
	.ToArray();

// Part 1
var mostCalories = input.Select(g => g.Sum())
	.OrderDescending()
	.First();

Console.WriteLine($"Part 1: {mostCalories}");

// Part 2
var topThreeMostCalories = input
	.Select(g => g.Sum())
	.OrderDescending()
	.Take(3)
	.Sum();

Console.WriteLine($"Part 2: {topThreeMostCalories}");