using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day11;

public sealed partial class InspectOperation
{
	public InspectOperationType Type { get; }

	/// <summary>
	/// The value that will be applied in the operation. If null, the passed-in value will be used.
	/// </summary>
	public int? Value { get; }

	public InspectOperation(InspectOperationType type, int? value)
	{
		Type = type;
		Value = value;
	}

	public static InspectOperation Parse(string input)
	{
		var match = ParseRegex().Match(input);

		var type = match.Groups["operator"].Value switch
		{
			"+" => InspectOperationType.Add,
			"*" => InspectOperationType.Multiply,
			_ => throw new FormatException(),
		};

		int? value = null;
		if (int.TryParse(match.Groups["value"].Value, out var parsedValue))
		{
			value = parsedValue;
		}

		return new InspectOperation(type, value);
	}

	[GeneratedRegex(@"new = old (?<operator>[+*]) (?<value>\d+|old)")]
	private static partial Regex ParseRegex();

	public long Apply(long item)
	{
		var value = Value ?? item;

		return Type switch
		{
			InspectOperationType.Add => item + value,
			InspectOperationType.Multiply => item * value,
			_ => throw new UnreachableException(),
		};
	}

	public override string ToString()
	{
		var operation = Type switch
		{
			InspectOperationType.Add => "+",
			InspectOperationType.Multiply => "*",
			_ => throw new UnreachableException(),
		};

		var value = Value?.ToString() ?? "old";

		return $"Operation: new = old {operation} {value}";
	}
}

public enum InspectOperationType
{
	Add,
	Multiply,
}