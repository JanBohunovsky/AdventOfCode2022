using System.Diagnostics;

namespace Day07;

[DebuggerDisplay("{Name} (dir, total_size={Size})")]
public class Directory : IContent
{
	private readonly List<IContent> _contents = new();

	public string Name { get; }
	public long Size => Contents.Sum(c => c.Size);

	public Directory? Parent { get; private set; }
	public IReadOnlyList<IContent> Contents => _contents;

	public Directory(string name)
	{
		Name = name;
	}

	public void AddContent(IContent content)
	{
		if (content is Directory directory)
		{
			directory.Parent = this;
		}

		_contents.Add(content);
	}

	public IEnumerable<Directory> GetAllDirectoriesRecursively()
	{
		yield return this;

		foreach (var directory in _contents.OfType<Directory>().SelectMany(d => d.GetAllDirectoriesRecursively()))
		{
			yield return directory;
		}
	}
}