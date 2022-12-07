namespace Day07.Commands;

public abstract class Command
{
	public abstract void Run(ref Directory current, Directory root);

	public static Command Parse(string[] data, ref int position)
	{
		var line = data[position].Split(' ', StringSplitOptions.RemoveEmptyEntries);
		if (line[0] is not "$")
		{
			throw new InvalidOperationException("Command must start with '$' character");
		}

		position++;

		return line[1] switch
		{
			"cd" => new ChangeDirectoryCommand(line[2]),
			"ls" => ListContentsCommand.Parse(data, ref position),
			_ => throw new Exception($"Unknown command '{line[1]}'"),
		};
	}

	public static List<Command> ParseAll(string[] data)
	{
		var commands = new List<Command>();

		var position = 0;
		while (position < data.Length)
		{
			commands.Add(Parse(data, ref position));
		}

		return commands;
	}
}