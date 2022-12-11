var input = File.ReadAllText("Data/input.txt")
	.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

// Part 1
var monkeys = ParseMonkeys();

for (int i = 0; i < 20; i++)
{
	ProcessRound();
}

Console.WriteLine($"Part 1: {CalculateMonkeyLevel()}");

// Part 2
monkeys = ParseMonkeys();
var testProduct = monkeys.Values
	.Select(m => m.InspectTest.Value)
	.Aggregate((x, y) => x * y);

for (int i = 0; i < 10_000; i++)
{
	ProcessRound(testProduct);
}

Console.WriteLine($"Part 2: {CalculateMonkeyLevel()}");


Dictionary<int, Monkey> ParseMonkeys()
{
	return input.Select(Monkey.Parse)
		.ToDictionary(m => m.Id, m => m);
}

long CalculateMonkeyLevel()
{
	return monkeys.Values
		.Select(m => (long)m.ItemsInspected)
		.OrderDescending()
		.Take(2)
		.Aggregate((x, y) => x * y);
}

void ProcessRound(int? testValuesProduct = null)
{
	for (int i = 0; i < monkeys.Count; i++)
	{
		var monkey = monkeys[i];

		while (true)
		{
			var result = monkey.Inspect(testValuesProduct);
			if (!result.Throw)
			{
				break;
			}

			monkeys[result.TargetMonkeyId].AddItem(result.Item);
		}
	}
}

