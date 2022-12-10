var input = File.ReadAllLines("Data/input.txt");

var cpu = CentralProcessingUnit.Parse(input);
var crt = new CathodeRayTube();
var signalStrengths = new List<int>();

do
{
	if ((cpu.Cycle - 20) % 40 is 0)
	{
		signalStrengths.Add(cpu.Cycle * cpu.RegisterX);
	}

	crt.Draw(cpu.RegisterX);
} while (cpu.Tick());

Console.WriteLine($"Part 1: {signalStrengths.Sum()}");
Console.WriteLine("Part 2:");
Console.WriteLine(crt.Image);