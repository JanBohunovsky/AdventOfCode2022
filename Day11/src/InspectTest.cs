using System.Text.RegularExpressions;

namespace Day11;

public sealed partial class InspectTest
{
	public int Value { get; }
	public int MonkeyTrue { get; }
	public int MonkeyFalse { get; }

	public InspectTest(int value, int monkeyTrue, int monkeyFalse)
	{
		Value = value;
		MonkeyTrue = monkeyTrue;
		MonkeyFalse = monkeyFalse;
	}

	public static InspectTest Parse(string input)
	{
		var match = ParseRegex().Match(input);

		var value = int.Parse(match.Groups["value"].Value);
		var monkeyTrue = int.Parse(match.Groups["true"].Value);
		var monkeyFalse = int.Parse(match.Groups["false"].Value);

		return new InspectTest(value, monkeyTrue, monkeyFalse);
	}

	[GeneratedRegex(@"divisible by (?<value>\d+)\n.+true: throw to monkey (?<true>\d+)\n.+false: throw to monkey (?<false>\d+)")]
	private static partial Regex ParseRegex();

	public int Test(long item)
	{
		return item % Value == 0 ? MonkeyTrue : MonkeyFalse;
	}

	public override string ToString()
	{
		return $"Test: divisible by {Value} ? monkey {MonkeyTrue} : monkey {MonkeyFalse}";
	}
}