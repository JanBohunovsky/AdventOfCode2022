var input = File.ReadAllLines("Data/input.txt");

var rope1 = new Rope();
var rope2 = new Rope(10);

foreach (var instruction in input)
{
	rope1.Move(instruction);
	rope2.Move(instruction);
}

Console.WriteLine($"Part 1: {rope1.TailVisitedPositions.Count()}");
Console.WriteLine($"Part 2: {rope2.TailVisitedPositions.Count()}");