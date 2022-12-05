using System.Text.RegularExpressions;

var input = File.ReadAllText("Data/input.txt")
	.Split("\n\n");

var stacks = ParseStacks(input[0].Split('\n'));

var instructions = input[1].Split('\n');
var instructionRegex = new Regex(@"move (?<count>\d+) from (?<source_id>\d+) to (?<target_id>\d+)",
	RegexOptions.Compiled | RegexOptions.ECMAScript);

foreach (var instruction in instructions)
{
	var match = instructionRegex.Match(instruction);
	var count = int.Parse(match.Groups["count"].Value);
	var sourceId = int.Parse(match.Groups["source_id"].Value);
	var targetId = int.Parse(match.Groups["target_id"].Value);

	// Part 1
	// ProcessInstructionOneByOne(stacks[sourceId], stacks[targetId], count);

	// Part 2
	ProcessInstructionAtOnce(stacks[sourceId], stacks[targetId], count);
}

var rawMessage = stacks
	.OrderBy(p => p.Key)
	.Select(p => p.Value.Peek())
	.ToArray();
var message = new string(rawMessage);

Console.WriteLine($"Result: {message}");


void ProcessInstructionOneByOne(Stack<char> source, Stack<char> target, int count)
{
	for (int i = 0; i < count; i++)
	{
		var crate = source.Pop();
		target.Push(crate);
	}
}

void ProcessInstructionAtOnce(Stack<char> source, Stack<char> target, int count)
{
	var holder = new List<char>();
	for (int i = 0; i < count; i++)
	{
		holder.Add(source.Pop());
	}

	for (int i = count - 1; i >= 0; i--)
	{
		target.Push(holder[i]);
	}
}

Dictionary<int, Stack<char>> ParseStacks(string[] rawStacks)
{
	var result = new Dictionary<int, Stack<char>>();
	var stackIds = rawStacks[^1]
		.Split(' ', StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse)
		.ToArray();

	for (int i = rawStacks.Length - 2; i >= 0; i--)
	{
		var line = rawStacks[i];

		for (int j = 0; j < stackIds.Length; j++)
		{
			var start = j * 4;
			var id = stackIds[j];

			if (line.Length < start + 3)
			{
				continue;
			}

			var rawValue = line.Substring(start, 3)
				.Trim();

			if (rawValue is "")
			{
				continue;
			}

			var value = rawValue[1];
			if (!result.TryGetValue(id, out var stack))
			{
				stack = new Stack<char>();
				result[id] = stack;
			}

			stack.Push(value);
		}
	}

	return result;
}