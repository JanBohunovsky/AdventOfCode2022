var input = System.IO.File.ReadAllLines("Data/input.txt");

// Parse commands
var commands = Command.ParseAll(input);

// Run commands
var root = new Directory("/");
var currentDirectory = root;

foreach (var command in commands)
{
	command.Run(ref currentDirectory, root);
}

// Part 1
var totalSizeOfSmallDirectories = root.GetAllDirectoriesRecursively()
	.Where(d => d.Size <= 100_000)
	.Sum(d => d.Size);

Console.WriteLine($"Part 1: {totalSizeOfSmallDirectories}");

// Part 2
const long totalDiskSpace = 70_000_000;
const long requiredFreeSpace = 30_000_000;
const long maximumAllowedTakenSpace = totalDiskSpace - requiredFreeSpace;

var sizeToDelete = root.Size - maximumAllowedTakenSpace;
if (sizeToDelete <= 0)
{
	Console.WriteLine("Part 2: Nothing to delete. Maybe you have invalid puzzle input?");
	return;
}

var directoryToDelete = root.GetAllDirectoriesRecursively()
	.Where(d => d.Size >= sizeToDelete)
	.OrderBy(d => d.Size)
	.First();

Console.WriteLine($"Part 2: {directoryToDelete.Size}");