using System.Diagnostics;

var input = File.ReadAllLines("Data/input.txt")
	.Select(l => new
	{
		Opponent = l[0] switch
		{
			'A' => Shape.Rock,
			'B' => Shape.Paper,
			'C' => Shape.Scissors,
			_ => throw new UnreachableException(),
		},
		You = l[2],
	})
	.ToArray();


// Part 1
var totalScoreForAssumedStrategy = input
	.Select(i => new
	{
		i.Opponent,
		You = i.You switch
		{
			'X' => Shape.Rock,
			'Y' => Shape.Paper,
			'Z' => Shape.Scissors,
			_ => throw new UnreachableException(),
		},
	})
	.Sum(i => CalculateScore(i.Opponent, i.You));

Console.WriteLine($"Part 1: {totalScoreForAssumedStrategy}");


// Part 2
var totalScoreForActualStrategy = input
	.Select(i => new
	{
		i.Opponent,
		You = i.You switch
		{
			'X' => ShapeHelper.GetLosingShape(i.Opponent),
			'Y' => i.Opponent,
			'Z' => ShapeHelper.GetWinningShape(i.Opponent),
			_ => throw new UnreachableException(),
		},
	})
	.Sum(i => CalculateScore(i.Opponent, i.You));

Console.WriteLine($"Part 2: {totalScoreForActualStrategy}");


int CalculateScore(Shape opponent, Shape you)
{
	var shapeScore = (int)you;
	int outcomeScore;

	if (opponent == you)
	{
		outcomeScore = 3;
	}
	else if (opponent.IsBeatenBy(you))
	{
		outcomeScore = 6;
	}
	else
	{
		outcomeScore = 0;
	}

	return shapeScore + outcomeScore;
}