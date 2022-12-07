using System.Diagnostics;

namespace Day07.Commands;

[DebuggerDisplay("cd {_target}")]
public class ChangeDirectoryCommand : Command
{
	private readonly string _target;

	public ChangeDirectoryCommand(string target)
	{
		_target = target;
	}

	public override void Run(ref Directory current, Directory root)
	{
		var target = _target switch
		{
			".." => current.Parent,
			"/" => root,
			_ => current.Contents
					.OfType<Directory>()
					.FirstOrDefault(d => d.Name == _target)
		};

		current = target ?? throw new InvalidOperationException("Could not determine target directory");
	}
}