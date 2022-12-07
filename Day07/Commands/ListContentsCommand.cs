using System.Diagnostics;

namespace Day07.Commands;

[DebuggerDisplay("ls")]
public class ListContentsCommand : Command
{
	private readonly IEnumerable<IContent> _contents;

	public ListContentsCommand(IEnumerable<IContent> contents)
	{
		_contents = contents;
	}

	public override void Run(ref Directory current, Directory root)
	{
		foreach (var content in _contents)
		{
			current.AddContent(content);
		}
	}

	public new static ListContentsCommand Parse(string[] data, ref int position)
	{
		var contents = new List<IContent>();

		while (true)
		{
			if (position >= data.Length)
			{
				break;
			}

			var fileLine = data[position].Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (fileLine[0] is "$")
			{
				break;
			}

			var name = fileLine[1];
			var sizeOrDir = fileLine[0];

			if (sizeOrDir is "dir")
			{
				contents.Add(new Directory(name));
			}
			else
			{
				contents.Add(new File(name, long.Parse(sizeOrDir)));
			}

			position++;
		}

		return new ListContentsCommand(contents);
	}
}