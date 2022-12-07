using System.Diagnostics;

namespace Day07;

[DebuggerDisplay("{Name} (file, size={Size})")]
public class File : IContent
{
	public string Name { get; }
	public long Size { get; }

	public File(string name, long size)
	{
		Name = name;
		Size = size;
	}
}