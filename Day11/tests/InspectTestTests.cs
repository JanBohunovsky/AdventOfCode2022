namespace Day11.Tests;

public sealed class InspectTestTests
{
	[Theory]
	[InlineData("divisible by 23\n  If true: throw to monkey 2\n  If false: throw to monkey 3", 23, 2, 3)]
	[InlineData("Test: divisible by 19\n  If true: throw to monkey 2\n  If false: throw to monkey 0", 19, 2, 0)]
	[InlineData("  Test: divisible by 17\n    If true: throw to monkey 0\n    If false: throw to monkey 1", 17, 0, 1)]
	public void Parsing_valid_input_returns_correct_object(string input, int value, int monkeyTrue, int monkeyFalse)
	{
		var result = InspectTest.Parse(input);

		result.Value.Should().Be(value);
		result.MonkeyTrue.Should().Be(monkeyTrue);
		result.MonkeyFalse.Should().Be(monkeyFalse);
	}

	[Theory]
	[InlineData("")]
	[InlineData("divisible by moon\nIf true: throw to monkey 1\nIf false: throw to monkey 2")]
	[InlineData("divisible by 12\nIf true: throw to monkey A\nIf false: throw to monkey B")]
	public void Parsing_invalid_input_throws_exception(string input)
	{
		var act = () => InspectTest.Parse(input);

		act.Should().Throw<FormatException>();
	}

	[Theory]
	[InlineData(5, 1, 0, 20, 1)]
	[InlineData(5, 1, 0, 21, 0)]
	[InlineData(2, 1, 0, 18572, 1)]
	[InlineData(2, 1, 0, 18571, 0)]
	public void Testing_item_returns_correct_monkey(int value, int monkeyTrue, int monkeyFalse, int item, int expectedResult)
	{
		var test = new InspectTest(value, monkeyTrue, monkeyFalse);

		var result = test.Test(item);

		result.Should().Be(expectedResult);
	}
}