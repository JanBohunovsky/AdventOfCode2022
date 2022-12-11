using System.Text.RegularExpressions;

namespace Day11;

public sealed partial class Monkey
{
	private readonly Queue<long> _items = new();

	public int Id { get; }
	public IEnumerable<long> Items => _items;
	public InspectOperation InspectOperation { get; }
	public InspectTest InspectTest { get; }

	public int ItemsInspected { get; private set; }

	public Monkey(int id, IEnumerable<int> startingItems, InspectOperation operation, InspectTest test)
	{
		Id = id;
		InspectOperation = operation;
		InspectTest = test;

		foreach (var item in startingItems)
		{
			_items.Enqueue(item);
		}
	}

	public static Monkey Parse(string input)
	{
		var match = ParseRegex().Match(input);

		var id = int.Parse(match.Groups["id"].Value);

		var items = match.Groups["items"].Value
			.Split(", ", StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse);

		var operation = InspectOperation.Parse(match.Groups["operation"].Value);
		var test = InspectTest.Parse(match.Groups["test"].Value);

		return new Monkey(id, items, operation, test);
	}

	[GeneratedRegex(@"Monkey (?<id>\d+):\n.*Starting items: (?<items>.+)\n.*Operation: (?<operation>.+)\n.*Test: (?<test>.+\n.+\n.+)")]
	private static partial Regex ParseRegex();

	public InspectResult Inspect(int? testValuesProduct = null)
	{
		if (!_items.TryDequeue(out var item))
		{
			return InspectResult.Finished();
		}

		item = InspectOperation.Apply(item);
		ItemsInspected++;

		if (testValuesProduct is null)
		{
			// Part 1: Divide worry level by 3
			item /= 3;
		}
		else
		{
			// Part 2: Reduce the worry level by the product of all test values.
			// This works because `(a % kn) % n` is the same thing as `a % n`,
			// where `a` is the input and `n` is the test value and k is a positive integer.
			item %= testValuesProduct.Value;
		}

		var targetMonkey = InspectTest.Test(item);

		return InspectResult.ThrowToMonkey(targetMonkey, item);
	}

	public void AddItem(long item)
	{
		_items.Enqueue(item);
	}

	public override string ToString()
	{
		var items = string.Join(", ", Items);
		return $"Monkey {Id}: [{items}]";
	}
}