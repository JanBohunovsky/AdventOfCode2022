namespace Day11.Tests;

public sealed class MonkeyTests
{
	[Theory]
	[InlineData("Monkey 0:\nStarting items: 79, 98\nOperation: new = old * 19\nTest: divisible by 23\nIf true: throw to monkey 2\nIf false: throw to monkey 3", 0, 79, 98)]
	[InlineData("Monkey 0:\nStarting items: 1\nOperation: new = old * 19\nTest: divisible by 23\nIf true: throw to monkey 2\nIf false: throw to monkey 3", 0, 1)]
	[InlineData("Monkey 1:\n  Starting items: 54, 65, 75, 74\n  Operation: new = old + 6\n  Test: divisible by 19\n    If true: throw to monkey 2\n    If false: throw to monkey 0", 1, 54, 65, 75, 74)]
	[InlineData("Monkey 2:\n  Starting items: 79, 60, 97\n  Operation: new = old * old\n  Test: divisible by 13\n    If true: throw to monkey 1\n    If false: throw to monkey 3", 2, 79, 60, 97)]
	public void Parsing_valid_input_returns_correct_object(string input, int expectedId, params int[] expectedStartingItems)
	{
		var result = Monkey.Parse(input);

		result.Id.Should().Be(expectedId);
		result.Items.Should().BeEquivalentTo(expectedStartingItems);
		result.InspectOperation.Should().NotBeNull();
		result.InspectTest.Should().NotBeNull();
	}

	[Theory]
	[InlineData("")]
	[InlineData("Monkey 0")]
	[InlineData("10, 20\nOperation: new = old * 19\nTest: divisible by 23\nIf true: throw to monkey 2\nIf false: throw to monkey 3")]
	[InlineData("Starting items: A, B\nOperation: new = old * 19\nTest: divisible by 23\nIf true: throw to monkey 2\nIf false: throw to monkey 3")]
	public void Parsing_invalid_input_throws_exception(string input)
	{
		var act = () => Monkey.Parse(input);

		act.Should().Throw<FormatException>();
	}

	[Fact]
	public void Monkey_throws_inspected_items_to_correct_monkeys()
	{
		var startingItems = new[] { 9, 22 };
		var operation = new InspectOperation(InspectOperationType.Add, 11);
		var test = new InspectTest(2, 1, 2);
		var monkey = new Monkey(0, startingItems, operation, test);

		var result = monkey.Inspect();
		result.Throw.Should().BeTrue();
		result.TargetMonkeyId.Should().Be(1);
		result.Item.Should().Be(6);

		result = monkey.Inspect();
		result.Throw.Should().BeTrue();
		result.TargetMonkeyId.Should().Be(2);
		result.Item.Should().Be(11);

		result = monkey.Inspect();
		result.Throw.Should().BeFalse();
	}

	[Fact]
	public void Adding_item_adds_it_to_the_end()
	{
		var items = new[] { 1, 2 };
		var monkey = new Monkey(0, items, null!, null!);

		monkey.AddItem(3);

		monkey.Items.Should().EndWith(3);
	}

	[Fact]
	public void Inspecting_item_increments_ItemsInspected_by_1()
	{
		var startingItems = new[] { 9, 22 };
		var operation = new InspectOperation(InspectOperationType.Add, 11);
		var test = new InspectTest(2, 1, 2);
		var monkey = new Monkey(0, startingItems, operation, test);

		// Inspect 9
		monkey.Inspect();
		monkey.ItemsInspected.Should().Be(1);

		// Inspect 22
		monkey.Inspect();
		monkey.ItemsInspected.Should().Be(2);

		// Nothing to inspect
		monkey.Inspect();
		monkey.ItemsInspected.Should().Be(2);
	}
}